using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Domain.Models;

namespace MvcMovie.Service.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<string> GetGenres();
        IEnumerable<Movie> GetMovies(string movieGenre, string name);
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
    }
}
