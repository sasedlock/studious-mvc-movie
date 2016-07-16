using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.DataAccess.Dals;
using MvcMovie.DataAccess.Interfaces;
using Moq;

namespace MvcMovie.DataAccessTests.Dals
{
    [TestClass]
    public class MovieDalTests
    {
        private Mock<IMvcMovieDbContext> _mockDbContext;
        private MovieDal _dal;


        [ClassInitialize]
        public void InitializeMovieDalTestsClass()
        {
            _mockDbContext = new Mock<IMvcMovieDbContext>();
            _dal = new MovieDal(_mockDbContext.Object);
        }

        [TestMethod]
        public void GetGenres_WithResults_ReturnsResults()
        {
            // ARRANGE

            // ACT

            // ASSERT

        }
    }
}
