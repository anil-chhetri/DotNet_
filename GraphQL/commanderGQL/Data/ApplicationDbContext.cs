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

        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)  //making nullable
                .HasForeignKey(p => p.PlatformId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder
                .Entity<Command>()
                .HasOne(c => c.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(c => c.PlatformId).OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}