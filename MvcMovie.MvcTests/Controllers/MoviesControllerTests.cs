using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
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
        public void Details_NullId_ReturnsBadRequest()
        {
            // Arrange

            // Act
            var result = _controller.Details(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.GetHashCode());
        }

        [TestMethod]
        public void Details_NoMovieFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns((Movie) null);

            // Act
            var result = _controller.Details(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound.GetHashCode());
        }

        [TestMethod]
        public void Details_MovieReturned_ReturnsViewWithMovie()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns(_movies.FirstOrDefault(m => m.Id == 1));

            // Act
            var result = _controller.Details(1) as ViewResult;
            var movieModel = result.ViewData.Model as Movie;

            // Assert
            Assert.AreEqual(_movies.FirstOrDefault(m => m.Id == 1), movieModel);
        }

        [TestMethod]
        public void Create_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var movieToCreate = new Movie();
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => movieToCreate, movieToCreate.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };
            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);

            _controller.ModelState.Clear();
            _controller.ModelState.Merge(modelBinder.ModelState);

            // Act
            var result = _controller.Create(movieToCreate) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewData.ModelState["Genre"].Errors.Count > 0);
            Assert.IsTrue(result.ViewData.ModelState["Price"].Errors.Count > 0);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Create_ValidModel_CallsServiceAndReturnsToIndex()
        {
            // Arrange
            var movieToCreate = _movies.FirstOrDefault();

            // Act
            var result = _controller.Create(movieToCreate) as RedirectToRouteResult;

            // Assert
            _mockService.Verify(s => s.AddMovie(movieToCreate), Times.Once);
            Assert.IsTrue(result.RouteValues.Values.Contains("Index"));
        }

        [TestMethod]
        public void Edit_NullId_ReturnsBadRequest()
        {
            // Arrange

            // Act
            var result = _controller.Edit((int?) null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.GetHashCode());
        }

        [TestMethod]
        public void Edit_NoMovieFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns((Movie)null);

            // Act
            var result = _controller.Edit(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound.GetHashCode());
        }

        [TestMethod]
        public void Edit_MovieReturned_ReturnsViewWithMovie()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns(_movies.FirstOrDefault(m => m.Id == 1));

            // Act
            var result = _controller.Edit(1) as ViewResult;
            var movieModel = result.ViewData.Model as Movie;

            // Assert
            Assert.AreEqual(_movies.FirstOrDefault(m => m.Id == 1), movieModel);
        }

        [TestMethod]
        public void Edit_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var movieToEdit = new Movie();
            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                    () => movieToEdit, movieToEdit.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };
            var binder = new DefaultModelBinder().BindModel(
                new ControllerContext(), modelBinder);

            _controller.ModelState.Clear();
            _controller.ModelState.Merge(modelBinder.ModelState);

            // Act
            var result = _controller.Edit(movieToEdit) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewData.ModelState["Genre"].Errors.Count > 0);
            Assert.IsTrue(result.ViewData.ModelState["Price"].Errors.Count > 0);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Edit_ValidModel_CallsServiceAndReturnsToIndex()
        {
            // Arrange
            var movieToEdit = _movies.FirstOrDefault();

            // Act
            var result = _controller.Edit(movieToEdit) as RedirectToRouteResult;

            // Assert
            _mockService.Verify(s => s.EditMovie(movieToEdit), Times.Once);
            Assert.IsTrue(result.RouteValues.Values.Contains("Index"));
        }

        [TestMethod]
        public void Delete_NullId_ReturnsBadRequest()
        {
            // Arrange

            // Act
            var result = _controller.Delete(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.GetHashCode());
        }

        [TestMethod]
        public void Delete_NoMovieFound_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns((Movie)null);

            // Act
            var result = _controller.Delete(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NotFound.GetHashCode());
        }

        [TestMethod]
        public void Delete_MovieReturned_ReturnsViewWithMovie()
        {
            // Arrange
            _mockService.Setup(s => s.GetMovieById(It.IsAny<int>())).Returns(_movies.FirstOrDefault(m => m.Id == 1));

            // Act
            var result = _controller.Delete(1) as ViewResult;
            var movieModel = result.ViewData.Model as Movie;

            // Assert
            Assert.AreEqual(_movies.FirstOrDefault(m => m.Id == 1), movieModel);
        }

        [TestMethod]
        public void Delete_CallsService()
        {
            // Arrange

            // Act
            _controller.DeleteConfirmed(1);

            // Assert
            _mockService.Verify(s => s.DeleteMovie(1), Times.Once);
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
