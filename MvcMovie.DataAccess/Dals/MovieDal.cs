using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.DataAccess.Context;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Dals
{
    public class MovieDal : IMovieDal
    {
        private readonly IMvcMovieDbContext _db;

        public MovieDal(IMvcMovieDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _db = dbContext;
        }

        public IEnumerable<string> GetGenres()
        {
            var genres = from m in _db.Movies
                         orderby m.Genre
                         select m.Genre;

            return genres.AsEnumerable();
        }

        public IEnumerable<Movie> GetMovies()
        {
            var movies = from m in _db.Movies
                         select m;

            return movies.AsEnumerable();
        }

        public Movie GetMovieById(int id)
        {
            return _db.Movies.Find(id);
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }

        public void EditMovie(Movie movie)
        {
            _db.SetModified(movie);
            _db.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _db.Movies.Find(id);
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }
    }
}
