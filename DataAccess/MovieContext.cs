using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("MovieConnection")
        {
            
        }
        public DbSet<Domain.Movie> Movies { get; set; }
    }
}
