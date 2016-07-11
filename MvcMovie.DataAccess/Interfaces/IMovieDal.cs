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
        IQueryable<string> GetGenres();
        IQueryable<Movie> GetMovies(string movieGenre, string name);
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
    }
}
