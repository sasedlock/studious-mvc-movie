using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Domain.Models;
using MvcMovie.Service.Services;

namespace MvcMovie.ServiceTests.Services
{
    [TestClass]
    public class MovieServiceTests
    {
        private Mock<IMovieDal> _mockDal;
        private MovieService _service;
        private Movie _pulpFiction;
        private Movie _bigLebowski;
        private Movie _batmanBegins;
        private List<Movie> _movies;

        [TestMethod]
        public void GetGenres_ReturnsAsEnumerable()
        {
            // ARRANGE
            _mockDal.Setup(d => d.GetGenres()).Returns(_movies.Select(m => m.Genre).AsQueryable());

            // ACT
            var result = _service.GetGenres();

            // ASSERT
            Assert.IsInstanceOfType(result,typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void GetMovies_ReturnsAsEnumerable()
        {
            // ARRANGE

            // ACT
            var result = _service.GetMovies(null, null);

            // ASSERT
            Assert.IsInstanceOfType(result,typeof(IEnumerable<Movie>));
        }

        [TestMethod]
        public void GetMovies_GenreAndNameNull_ReturnsAllMovies()
        {
            // ARRANGE

            // ACT
            var result = _service.GetMovies(null, null);

            // ASSERT   
            CollectionAssert.AreEqual(_movies,result.ToList());
        }

        [TestMethod]
        public void GetMovies_GenreAndNameEmpty_ReturnsAllMovies()
        {
            // ARRANGE

            // ACT
            var result = _service.GetMovies(string.Empty, string.Empty);

            // ASSERT   
            CollectionAssert.AreEqual(_movies, result.ToList());
        }

        [TestMethod]
        public void GetMovies_GenreProvided_ReturnsFilteredSet()
        {
            // ARRANGE
            var genreName = "Action";
            
            // ACT
            var result = _service.GetMovies(genreName, null);

            // ASSERT
            CollectionAssert.IsSubsetOf(result.ToList(), _movies);
            Assert.AreEqual(_movies.Count(m => m.Genre == genreName),result.Count());
        }

        [TestMethod]
        public void GetMovies_NameProvided_ReturnsFilteredSet()
        {
            // ARRANGE
            var movieName = "Pulp";

            // ACT
            var result = _service.GetMovies(null, movieName);

            // ASSERT
            CollectionAssert.IsSubsetOf(result.ToList(), _movies);
            Assert.AreEqual(_movies.Count(m => m.Title.Contains(movieName)), result.Count());
        }

        [TestMethod]
        [Ignore]
        public void AddingMovie_ExistingName_ReturnsAppropriateError()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [TestMethod]
        [Ignore]
        public void TestTemplate()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [TestInitialize]
        public void Initialize()
        {
            _pulpFiction = new Movie
            {
                Id = 1,
                Genre = "Action",
                Price = (decimal) 9.99,
                Rating = "R",
                ReleaseDate = DateTime.Now,
                Title = "Pulp Fiction"
            };

            _bigLebowski = new Movie
            {
                Id = 2,
                Genre = "Comedy",
                Price = (decimal) 9.99,
                Rating = "R",
                ReleaseDate = DateTime.Now,
                Title = "The Big Lebowski"
            };

            _batmanBegins = new Movie
            {
                Id = 3,
                Genre = "Action",
                Price = (decimal)9.99,
                Rating = "PG-13",
                ReleaseDate = DateTime.Now,
                Title = "Batman Begins"
            };

            _movies = new List<Movie>
            {
                _pulpFiction,
                _bigLebowski,
                _batmanBegins  
            };

            _mockDal = new Mock<IMovieDal>();
            _mockDal.Setup(d => d.GetMovies()).Returns(_movies.AsQueryable());
            _service = new MovieService(_mockDal.Object);
        }
    }
}
