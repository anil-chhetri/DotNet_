using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataExamples.Data
{
    public class ODataDbContext : DbContext
    {
        public ODataDbContext(DbContextOptions<ODataDbContext> options) 
            : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Order> Orders { get; set; }

    }
}
