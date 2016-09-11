using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    public class MovieDal
    {
        private MovieContext _context = new MovieContext();

        #region Queries

        public IEnumerable<Movie> GetMovies()
        {
            var movies = from m in _context.Movies
                            select m;

            return movies.AsEnumerable();
        }

        public IEnumerable<string> GetGenres()
        {
            var genres = from m in _context.Movies
                            orderby m.Genre
                            select m.Genre;

            return genres.Distinct().AsEnumerable();
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.Find(id);
        }

        #endregion

        #region Commands

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void EditMovie(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movieToDelete = _context.Movies.Find(id);
            _context.Movies.Remove(movieToDelete);
            _context.SaveChanges();
        }

        #endregion
    }
}
