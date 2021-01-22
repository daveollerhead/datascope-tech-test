using DatascopeTest.Data.Config;
using DatascopeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DatascopeTest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}