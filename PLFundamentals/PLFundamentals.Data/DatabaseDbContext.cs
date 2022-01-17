using Microsoft.EntityFrameworkCore;
using PLFundamentals.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLFundamentals.Data
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
