using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMovie.Controllers;
using MvcMovie.DataAccess.Dals;
using MvcMovie.Service.Services;

namespace MvcMovie.IntegrationTests
{
    [TestClass]
    public class MovieIntegration
    {
        private MoviesController _controller;
        private MovieService _service;
        private MovieDal _dal;

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestCleanup]
        public void Cleanup()
        {
            
        }
    }
}
