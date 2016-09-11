using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;

namespace Service
{
    public class MovieService
    {
        private MovieDal _dal = new MovieDal();

        #region Queries

        public IEnumerable<Movie> GetMovies()
        {
            return _dal.GetMovies().ToList();
        }

        public IEnumerable<string> GetGenres()
        {
            return _dal.GetGenres().ToList();
        }

        public Movie GetMovieById(int id)
        {
            return _dal.GetMovieById(id);
        }

        public IEnumerable<Movie> GetMoviesByNameAndGenre(string name, string genre)
        {
            var movies = _dal.GetMovies();

            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(m => m.Title.Contains(name));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.Genre == genre);
            }

            return movies.ToList();
        }

        #endregion

        #region Commands

        public void AddMovie(Movie movie)
        {
            if (MovieIsValid(movie))
            {
                _dal.AddMovie(movie);
            }
        }

        public void EditMovie(Movie movie)
        {
            if (MovieIsValid(movie))
            {
                _dal.EditMovie(movie);
            }
        }

        public void DeleteMovie(int id)
        {
            if (_dal.GetMovieById(id) != null)
            {
                _dal.DeleteMovie(id);
            }
        }

        #endregion

        #region Helpers

        private bool MovieIsValid(Movie movie)
        {
            return !string.IsNullOrEmpty(movie.Genre) 
                && !string.IsNullOrEmpty(movie.Rating) 
                && !string.IsNullOrEmpty(movie.Title);
        }

        #endregion
    }
}
