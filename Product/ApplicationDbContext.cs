using Microsoft.EntityFrameworkCore;

namespace Product
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<PassedTest> PassedTests { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=physicaltraining;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>().HasData(new Teacher() { Id = 1, Login = "mrc", Password = "mrc123!" });
        }
    }
}
