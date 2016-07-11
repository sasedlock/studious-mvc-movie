using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Context
{
    public class MvcMovieDbContext : DbContext, IMvcMovieDbContext
    {
        public MvcMovieDbContext() : base("MvcMovie")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbEntityEntry Entry(Movie movie)
        {
            return base.Entry(movie);
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
