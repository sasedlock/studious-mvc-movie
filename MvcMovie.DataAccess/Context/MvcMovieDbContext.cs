using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcMovie.Domain.Models;

namespace MvcMovie.DataAccess.Context
{
    public class MvcMovieDbContext : DbContext
    {
        public MvcMovieDbContext() : base("MvcMovie")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
