using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Interfaces
{
    public interface IMovieDal
    {
        IEnumerable<string> GetGenres();
        IEnumerable<Movie> GetMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
    }
}
