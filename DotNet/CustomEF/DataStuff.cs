using Microsoft.EntityFrameworkCore;

namespace CustomEF
{
    public class Item
    {
        public string Name { get; set; }        
    }

    public class EFCoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("dbName");
            //optionsBuilder.UseLocalDatabase("App_Data");
            optionsBuilder.UseSimple();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(e => e.Name);
        }
    }
}