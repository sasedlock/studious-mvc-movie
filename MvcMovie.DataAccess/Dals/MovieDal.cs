using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.DataAccess.Context;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Dals
{
    public class MovieDal
    {
        private MvcMovieDbContext db = new MvcMovieDbContext();

        public IQueryable<string> GetGenres()
        {
            var genres = from m in db.Movies
                         orderby m.Genre
                         select m.Genre;

            return genres.AsQueryable();
        }

        public IQueryable<Movie> GetMovies(string movieGenre, string name)
        {
            var movies = from m in db.Movies
                         select m;

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.Find(id);
        }

        public void AddMovie(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void EditMovie(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
