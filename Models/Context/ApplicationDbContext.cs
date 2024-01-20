using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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
            modelBuilder.Entity<Semester>().HasData(new Semester { SemesterId = 1, SemesterName = "Term One" },
                new Semester { SemesterId = 2, SemesterName = "Term Two" }, new Semester { SemesterId = 3, SemesterName = "Term Three" });
            modelBuilder.Entity<Student>()
     .Property(p => p.SchoolFees)
     .HasColumnType("decimal(10, 2)");
            modelBuilder.Entity<SchoolLevel>().HasData(
                new SchoolLevel { LevelName = "Form One", SchoolLevelId = 1 },new SchoolLevel { LevelName = "Form Two", SchoolLevelId = 2},
                new SchoolLevel { LevelName = "Form Three", SchoolLevelId = 3 }, new SchoolLevel { LevelName = "Form Four", SchoolLevelId = 4 }, new SchoolLevel{ LevelName = "Form Five", SchoolLevelId = 5 }, new SchoolLevel { LevelName = "Form Six", SchoolLevelId = 6 });
          

                
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<LibItem> LibItems { get; set; }
        public DbSet<LibUser> LibUsers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GeneralLedger> GeneralLedger { get; set; }
        public DbSet<SchoolLevel> SchoolLevels { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<FeesPayment> FeesPayments { get; set; }
        public DbSet<LevelData> LevelDatas { get; set; }
        public DbSet<LevelChange> LevelChanges { get; set; }
        public DbSet<Assessement> Assessements { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<ReportCard> ReportCards { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentAssessemet> StudentAssessemets { get; set; }
        public DbSet<AssessedStudent> AssessedStudents { get; set; }

        public async Task NewLevelData(Student student, ApplicationDbContext _dbL)
        {
            var LevelChange = new LevelChange            {
                StudentId = student.StudentId,
                schoolFess = student.SchoolFees,
                SchoolLevel = student.SchoolLevel,
                Semester = student.Semester,
            };
            await _dbL.LevelChanges.AddAsync(LevelChange);
            await _dbL.SaveChangesAsync();
        }
        public async Task CreateAssessedStudent(int Id,ApplicationDbContext _dbf)
        {
            var students = await _dbf.AssessedStudents.ToListAsync();
            var levelChanges = await _dbf.LevelChanges.Include(p=>p.SchoolLevel).Include(k=>k.Semester).ToListAsync();
            var levelDatas = await _dbf.LevelDatas.Where(p => p.StudentId == Id).ToListAsync();
            var StudentLevelData = levelDatas.FirstOrDefault();
            var studentData= levelDatas.Where(p=>p.StudentId==Id).ToList().LastOrDefault();
            _dbf.AssessedStudents.RemoveRange(students);
            await _dbf.SaveChangesAsync();

                var AssessedStudent = new AssessedStudent();
                AssessedStudent.StudentId= Id;
            AssessedStudent.Semester = await _dbf.Semesters.FindAsync(studentData.SemesterId);
            AssessedStudent.SchoolLevel = await _dbf.SchoolLevels.FindAsync(studentData.SchoolLevelId);
                await _dbf.AssessedStudents.AddAsync(AssessedStudent);
                await _dbf.SaveChangesAsync();
        }
        public async Task NewStudentFeesLedger(Student student,ApplicationDbContext _dbF)
        {
            var Ledger = new FeesPayment
            {
                StudentId = student.StudentId,
                Semester = student.Semester,
                Balance = student.SchoolFees,
                StudentLevel = student.SchoolLevel,
                Reason = "New student registration",
                RecievedBy="Not Paid",
            };
            await _dbF.FeesPayments.AddAsync(Ledger);
            await _dbF.SaveChangesAsync();



        }
        public async Task UpdatePromotedStudentdata(Student student, ApplicationDbContext _dbF)
        {
            var levelData = await _dbF.LevelDatas.ToListAsync();
            var studentdata = (from data in levelData where data.StudentId == student.StudentId select data).ToList().FirstOrDefault();
            if (studentdata!= null)
            {
                studentdata.SchoolLevelId = student.SchoolLevel.SchoolLevelId;
                studentdata.SemesterId = student.Semester.SemesterId;
                await _dbF.SaveChangesAsync();

            }
            else
            {
                var newStudentData = new LevelData();
                newStudentData.SemesterId = student.Semester.SemesterId;
                newStudentData.SchoolLevelId = student.SchoolLevel.SchoolLevelId;
                newStudentData.StudentId = student.StudentId;
                await _dbF.LevelDatas.AddAsync(newStudentData);
                await _dbF.SaveChangesAsync();
            }

        }
    }
}
