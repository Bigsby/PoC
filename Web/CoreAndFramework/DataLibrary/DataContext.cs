using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var item = modelBuilder.Entity<Item>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
