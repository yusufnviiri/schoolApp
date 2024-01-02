using Microsoft.EntityFrameworkCore;
namespace schoolApp.Models.Context
{
    public class ApplicationDbContext:DbContext
    {        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
     .Property(p => p.SchoolFees)
     .HasColumnType("decimal(10, 2)");
        }
        public DbSet<Student> Students { get; set; }
    }
}
