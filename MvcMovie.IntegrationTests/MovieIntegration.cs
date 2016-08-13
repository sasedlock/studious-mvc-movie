using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.Controllers;
using MvcMovie.DataAccess.Context;
using MvcMovie.DataAccess.Dals;
using MvcMovie.Domain.Models;
using MvcMovie.Service.Services;

namespace MvcMovie.IntegrationTests
{
    [TestClass]
    public class MovieIntegration
    {
        private MoviesController _controller;
        private MovieService _service;
        private MovieDal _dal;
        private MvcMovieDbContext _context;
        private Movie _movieToCreate;
        private List<Movie> _existingMovies;

        [TestMethod]
        public void AddingMovie_ValidMovie_ShouldIncrementCountOfMoviesInDb()
        {
            // Arrange
            var currentMovieCount = _dal.GetMovies().Count();

            // Act
            _controller.Create(_movieToCreate);

            // Assert
            var newMovieCount = _dal.GetMovies().Count();
            Assert.AreEqual(1, newMovieCount - currentMovieCount);
        }

        [TestMethod]
        public void UpdatingMovie_ValidMovie_ShouldUpdateMovieInDb()
        {
            // Arrange
            _context.Movies.Add(_movieToCreate);
            _context.SaveChanges();
            var updatedTitle = "The Dearly Departed";
            var updatedMovie = _movieToCreate;
            updatedMovie.Title = updatedTitle;

            // Act
            _controller.Edit(updatedMovie);

            // Assert
            var updatedMovieInDb = _context.Movies.Find(_movieToCreate.Id);
            Assert.AreEqual(updatedTitle, updatedMovieInDb.Title);
        }

        [TestMethod]
        public void IndexMethod_NoFilter_ShouldReturnAllMoviesInDb()
        {
            // Arrange
            _context.Movies.AddRange(_existingMovies);
            _context.SaveChanges();

            // Act
            var indexResult = _controller.Index(null, null) as ViewResult;
            var moviesReturned = indexResult.Model as List<Movie>;

            // Assert
            Assert.AreEqual(2, moviesReturned.Count);
        }

        [TestInitialize]
        public void Initialize()
        {
            _context = new MvcMovieDbContext("MvcMovieTest");
            _dal = new MovieDal(_context);
            _service = new MovieService(_dal);
            _controller = new MoviesController(_service);

            _movieToCreate = new Movie
            {
                Title = "The Departed",
                ReleaseDate = DateTime.Parse("2006-01-01"),
                Genre = "Suspense Thriller",
                Price = 19.99M,
                Rating = "R"
            };

            var otherMovie = new Movie
            {
                Title = "Star Wars: Episode IV - A New Hope",
                ReleaseDate = DateTime.Parse("1976-01-01"),
                Genre = "Action",
                Price = 19.99M,
                Rating = "PG"
            };

            _existingMovies = new List<Movie>
            {
                _movieToCreate, otherMovie
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM Movies");
        }
    }
}
