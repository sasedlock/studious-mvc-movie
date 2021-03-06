﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.DataAccess.Context;
using MvcMovie.DataAccess.Dals;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Domain.Models;
using MvcMovie.Service.Interfaces;

namespace MvcMovie.Service.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieDal _dal;

        public MovieService(IMovieDal movieDal)
        {
            if (movieDal == null)
            {
                throw new ArgumentNullException(nameof(movieDal));
            }

            _dal = movieDal;
        }

        public IEnumerable<string> GetGenres()
        {
            return _dal.GetGenres().AsEnumerable();
        }

        public IEnumerable<Movie> GetMovies(string movieGenre, string name)
        {
            var movies = _dal.GetMovies();

            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(s => s.Title.Contains(name));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(m => m.Genre == movieGenre);
            }

            return movies.AsEnumerable();
        }

        public Movie GetMovieById(int id)
        {
            return _dal.GetMovieById(id);
        }

        public void AddMovie(Movie movie)
        {
            _dal.AddMovie(movie);
        }

        public void EditMovie(Movie movie)
        {
            _dal.EditMovie(movie);
        }

        public void DeleteMovie(int id)
        {
            _dal.DeleteMovie(id);
        }
    }
}
