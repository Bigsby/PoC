using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
    }
}