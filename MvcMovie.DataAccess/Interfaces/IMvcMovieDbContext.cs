using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Interfaces
{
    public interface IMvcMovieDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbEntityEntry Entry(Movie movie);
        void SetModified(Movie movie);
        void SaveChanges();
    }
}