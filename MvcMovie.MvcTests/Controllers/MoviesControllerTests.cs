using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcMovie.Controllers;
using MvcMovie.Domain.Models;
using MvcMovie.Service.Interfaces;

namespace MvcMovie.MvcTests.Controllers
{
    [TestClass]
    public class MoviesControllerTests
    {
        private Mock<IMovieService> _mockService;
        private IEnumerable<Movie> _movies;
        private IEnumerable<string> _genres; 
        private MoviesController _controller;

        [TestMethod]
        public void Index_RetreivedMovies_ReturnsViewWithMovies()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovies(null, null)).Returns(_movies);
            _mockService.Setup(s => s.GetGenres()).Returns(_movies.Select(m => m.Genre));

            // Act
            var result = _controller.Index(null, null) as ViewResult;
            var movies = result.ViewData.Model as IEnumerable<Movie>;

            // Assert
            CollectionAssert.AreEqual(_movies.ToList(),movies.ToList());
        }

        [TestMethod]
        public void Index_RetrievedGenres_AddsToViewBagData()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovies(null, null)).Returns(_movies);
            _mockService.Setup(s => s.GetGenres()).Returns(_movies.Select(m => m.Genre));

            // Act
            var result = _controller.Index(null, null) as ViewResult;
            var genres = result.ViewData["movieGenre"];

            // Assert
            Assert.IsNotNull(genres);
        }

        [TestMethod]
        [Ignore]
        public void Details_NullId_ReturnsBadRequest()
        {
            
        }

        [TestInitialize]
        public void Init()
        {
            _movies = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Genre = "Action",
                    Price = (decimal) 9.99,
                    Rating = "R",
                    ReleaseDate = DateTime.Now,
                    Title = "Pulp Fiction"
                },
                new Movie
                {
                    Id = 2,
                    Genre = "Comedy",
                    Price = (decimal) 9.99,
                    Rating = "R",
                    ReleaseDate = DateTime.Now,
                    Title = "The Big Lebowski"
                },
                new Movie
                {
                    Id = 3,
                    Genre = "Action",
                    Price = (decimal)9.99,
                    Rating = "PG-13",
                    ReleaseDate = DateTime.Now,
                    Title = "Batman Begins"
                }
            };

            _mockService = new Mock<IMovieService>();
            _controller = new MoviesController(_mockService.Object);
        }
    }
}
