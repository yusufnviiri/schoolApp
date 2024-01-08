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
            modelBuilder.Entity<SchoolLevel>().HasData(
                new SchoolLevel { LevelName = "Form One", SchoolLevelId = 1 },new SchoolLevel { LevelName = "Form Two", SchoolLevelId = 2},
                new SchoolLevel { LevelName = "Form Three", SchoolLevelId = 3 }, new SchoolLevel { LevelName = "Form Four", SchoolLevelId = 4 }, new SchoolLevel{ LevelName = "Form Five", SchoolLevelId = 5 }, new SchoolLevel { LevelName = "Form Six", SchoolLevelId = 6 }); 
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolLevel> SchoolLevels { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<FeesPayment> FeesPayments { get; set; }
        public DbSet<LevelData> LevelDatas { get; set; }
        public DbSet<LevelChange> LevelChanges { get; set; }


        public async Task NewLevelData(Student student, ApplicationDbContext _dbL)
        {
            var LevelChange = new LevelChange            {
                StudentId = student.StudentId,
                schoolFess = student.SchoolFees,
                LevelName = student.SchoolLevel.LevelName,
                Semester = student.Semester,
            };
            await _dbL.LevelChanges.AddAsync(LevelChange);
            await _dbL.SaveChangesAsync();
        }

        public async Task NewStudentFeesLedger(Student student,ApplicationDbContext _dbF)
        {
            var Ledger = new FeesPayment
            {
                StudentId = student.StudentId,
                Semester = student.Semester,
                Balance = student.SchoolFees,
                StudentLevel = student.SchoolLevel.LevelName,
                Reason = "New student registration",
                RecievedBy="Not Paid",
            };
            await _dbF.FeesPayments.AddAsync(Ledger);
            await _dbF.SaveChangesAsync();



        }
    }
}
