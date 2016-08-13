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
        public MvcMovieDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbEntityEntry Entry(Movie movie)
        {
            return base.Entry(movie);
        }

        public void SetModified(Movie movie)
        {
            Entry(movie).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
