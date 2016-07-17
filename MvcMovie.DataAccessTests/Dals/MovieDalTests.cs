using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.DataAccess.Dals;
using MvcMovie.DataAccess.Interfaces;
using Moq;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccessTests.Dals
{
    [TestClass]
    public class MovieDalTests
    {
        private Mock<IMvcMovieDbContext> _mockDbContext;
        private MovieDal _dal;
        private IQueryable<Movie> _movies;
        private Mock<DbSet<Movie>> _mockMovieDbSet;

        [TestMethod]
        public void GetGenres_WithResults_ReturnsResults()
        {
            // ARRANGE

            // ACT
            var result = _dal.GetGenres();

            var expectedCount = 2;
            var actualCount = result.Count();

            var expectedList = _movies.ToList().Select(m => m.Genre).ToList();
            var actualList = result.ToList();

            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetMovies_WithResults_ReturnsResults()
        {
            // ARRANGE
            
            // ACT
            var result = _dal.GetMovies();

            var expectedCount = 2;
            var actualCount = result.Count();

            var expectedList = _movies.ToList();
            var actualList = result.ToList();

            // ASSERT
            Assert.AreEqual(expectedCount, actualCount);
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetMovieById_WithManyMoviesInContext_ReturnsSingleMovie()
        {
            // ARRANGE
            _mockMovieDbSet.Setup(m => m.Find(_movies.FirstOrDefault().Id)).Returns(_movies.FirstOrDefault());
            var movieToFind = _movies.FirstOrDefault();

            // ACT
            var result = _dal.GetMovieById(movieToFind.Id);

            // ASSERT
            Assert.AreEqual(movieToFind, result);
        }

        [TestMethod]
        public void AddMovie_ValidMoviePassed_AddsMovieToContextAndSavesChanges()
        {
            // ARRANGE
            var movieToAdd = new Movie
            {
                Id = 3,
                Genre = "Action",
                Price = (decimal) 10.99,
                Rating = "PG-13",
                ReleaseDate = DateTime.Now,
                Title = "The Matrix"
            };

            // ACT
            _dal.AddMovie(movieToAdd);

            // ASSERT
            _mockMovieDbSet.Verify(m => m.Add(movieToAdd), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void EditMovie_ValidMoviePassed_UpdatesEntityStateAndSavesChanges()
        {
            // ARRANGE
            var movieToUpdate = _movies.FirstOrDefault();
            movieToUpdate.Price = (decimal) 5.99;

            // ACT 
            _dal.EditMovie(movieToUpdate);

            // ASSERT
            _mockDbContext.Verify(m => m.SetModified(movieToUpdate), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteMovie_ValidMovie_RemovesMovieFromContextAndSavesChanges()
        {
            // ARRANGE
            var movieToDelete = _movies.FirstOrDefault();
            _mockMovieDbSet.Setup(m => m.Find(movieToDelete.Id)).Returns(movieToDelete);

            // ACT
            _dal.DeleteMovie(movieToDelete.Id);

            // ASSERT
            _mockMovieDbSet.Verify(m => m.Remove(movieToDelete), Times.Once);
            _mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestInitialize]
        public void InitializeMovieDalTestsClass()
        {
            // Initialize models
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
                }
            }.AsQueryable();

            // Initialize mock of Movies DbSet
            _mockMovieDbSet = new Mock<DbSet<Movie>>();
            _mockMovieDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(_movies.Provider);
            _mockMovieDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(_movies.Expression);
            _mockMovieDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(_movies.ElementType);
            _mockMovieDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(_movies.GetEnumerator());

            _mockDbContext = new Mock<IMvcMovieDbContext>();
            _mockDbContext.Setup(m => m.Movies).Returns(_mockMovieDbSet.Object);

            // Initialize Dal to test
            _dal = new MovieDal(_mockDbContext.Object);
        }
    }
}
