using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Domain.Models;

namespace MvcMovie.Service.Services
{
    public class MovieService
    {
        public IEnumerable<String> GetGenres()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMovies(string movieGenre, string name)
        {
            throw new NotImplementedException();
        }

        public Movie GetMovieById(int? id)
        {
            throw new NotImplementedException();
        }

        public void AddMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void EditMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
