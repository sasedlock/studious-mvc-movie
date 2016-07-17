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
            _mockDbContext.Setup(db => db.Movies).Returns(_mockMovieDbSet.Object);

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
