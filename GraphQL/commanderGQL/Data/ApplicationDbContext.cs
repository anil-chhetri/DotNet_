using commanderGQL.Models;
using Microsoft.EntityFrameworkCore;

namespace commanderGQL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) 
        {}

        public DbSet<Platform> Platforms { get; set; }
    }
}