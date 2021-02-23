using Microsoft.EntityFrameworkCore;

namespace Product
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<PassedTest> PassedTests { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=diploma;Trusted_Connection=True;");
        }
    }
}
