using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolApp.Models;
using schoolApp.Models.Context;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace schoolApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ICollection<Student> Students { get; set; }
        
        public Student StudentO { get; set; }
        public LevelChange LevelChange { get; set; } = new LevelChange();
        public LevelData LevelData { get; set; } = new LevelData();
        public FeesPayment transaction { get; set; } = new FeesPayment();
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public ODatamanager ODatamanager { get; set; } = new ODatamanager();
        //index
        public async Task<IActionResult> Index()
        {
            Students = await _db.Students.Include(p => p.SchoolLevel).Include(k => k.Semester).ToListAsync();
            var levelDatas = await _db.LevelDatas.ToListAsync();
            var semesters = await _db.Semesters.ToListAsync();
            var levels = await _db.SchoolLevels.ToListAsync();

            var newData = (from stdnt in Students
                           join datas in levelDatas on stdnt.StudentId equals datas.StudentId
                           select new FeesJoinStudent
                           {
                               FirstName = stdnt.FirstName,
                               LastName = stdnt.LastName,
                               SchoolLevel = levels.Where(k => k.SchoolLevelId == datas.SchoolLevelId).FirstOrDefault(),
                               Semester = semesters.Where(p => p.SemesterId == datas.SemesterId).FirstOrDefault(),
                               Sex = stdnt.Sex,
                               Residance = stdnt.Residance,
                               StudentId = stdnt.StudentId
                           }).ToList();

            return View(newData);
        }
        //create a student get
        public async Task<IActionResult> Create()
        {
            ODatamanager.schoolLevels = await _db.SchoolLevels.ToListAsync();
            ODatamanager.Semesters = await _db.Semesters.ToListAsync();
            return View(ODatamanager);
        }

        //create a student (post)

        [HttpPost]
        public async Task<IActionResult> Create(ODatamanager collection)
        {
            await _db.Guardians.AddAsync(collection.student.Guardian);
            await _db.SaveChangesAsync();
            var Guardian = await _db.Guardians.ToListAsync();
            collection.student.Guardian = Guardian.LastOrDefault();
            collection.student.SchoolLevel = await _db.SchoolLevels.FindAsync(collection.SchoolLevelId);
            collection.student.Semester = await _db.Semesters.FindAsync(collection.SemesterId);
            await _db.Students.AddAsync(collection.student);
            await _db.SaveChangesAsync();
            Students = await _db.Students.ToListAsync();
            var student = Students.LastOrDefault();
            // add stdent Data to Level data
            await _db.UpdatePromotedStudentdata(student, _db);

            await _db.LevelDatas.AddAsync(LevelData);
            await _db.SaveChangesAsync();
            //create a level Change

            await _db.NewLevelData(student, _db);
            //create student ledger
            await _db.NewStudentFeesLedger(student, _db);




            return RedirectToAction("Index");
        }
        //       pay school fees get

        public async Task<IActionResult> PayFees(int Id)
        {
            var transactions = await _db.FeesPayments.Include(p => p.Semester).Include(p => p.StudentLevel).ToListAsync();
            var levelChangeData = await _db.LevelChanges.Include(p => p.Semester).ToListAsync();

            var students = await _db.Students.Include(t => t.SchoolLevel).ToListAsync();
            var transact = (from std in students
                            join tr in transactions on std.StudentId equals tr.StudentId
                            select new FeesJoinStudent
                            {
                                FirstName = std.FirstName,
                                LastName = std.LastName,
                                Semester = levelChangeData.LastOrDefault().Semester,
                                SemesterId = levelChangeData.LastOrDefault().Semester.SemesterId,
                                Sex = std.Sex,
                                Balance = tr.Balance,
                                PrevBalance = tr.PrevBalance,
                                SchoolLevel = tr.StudentLevel,
                                SchoolFees = levelChangeData.LastOrDefault().schoolFess,
                                StudentId = std.StudentId,
                                LevelId = tr.StudentLevel.SchoolLevelId,

                            }).ToList().LastOrDefault();
            var transaction = (from trans in transactions where trans.StudentId == Id select trans).LastOrDefault();
            return View(transact);

        }
        //       pay school fees post
        [HttpPost]
        public async Task<IActionResult> PayFees(FeesJoinStudent transaction)
        {
            var ledgerBalances = new GeneralLedger();
            var lastTransaction = await _db.GeneralLedger.ToListAsync();
            
            // compute fees balaces
            var student = await _db.Students.FindAsync(transaction.StudentId);
            var newTrans = new FeesPayment();
            newTrans.StudentId = student.StudentId;
            newTrans.Semester = transaction.Semester;
            newTrans.PrevBalance = transaction.Balance;
            newTrans.Balance = transaction.Balance - transaction.Amount;
            newTrans.RecievedBy = transaction.RecievedBy;
            newTrans.Amount = transaction.Amount;
            newTrans.StudentLevel = await _db.SchoolLevels.FindAsync(transaction.LevelId);
            newTrans.Reason = transaction.Reason;
            newTrans.Semester = await _db.Semesters.FindAsync(transaction.SemesterId);
            await _db.FeesPayments.AddAsync(newTrans);
            ledgerBalances.Amount = newTrans.Amount;
            ledgerBalances.Reason = newTrans.Reason;
            if (lastTransaction.LastOrDefault() != null)
            {
                ledgerBalances.TotalIncome = lastTransaction.LastOrDefault().TotalIncome+ newTrans.Amount;
            }
            else
            {
                ledgerBalances.TotalIncome = newTrans.Amount;
            }
           
            await _db.GeneralLedger.AddAsync(ledgerBalances);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // register new semester get
        public async Task<IActionResult> NewSemesterReg(int Id)
        {
            var AllStudents = await _db.Students.Include(p => p.SchoolLevel).ToListAsync();
            StudentO = (from std in AllStudents where std.StudentId == Id select std).LastOrDefault();
            StudentO.Semester = new Semester();
            StudentO.SchoolFees = 0;
            StudentO.SchoolLevel = null;
            ODatamanager.student = StudentO;
            var levels = await _db.SchoolLevels.ToListAsync();
            ODatamanager.schoolLevels = levels;
            ODatamanager.Semesters = await _db.Semesters.ToListAsync();
            return View(ODatamanager);
        }
        // register new semester post

        [HttpPost]
        public async Task<IActionResult> NewSemesterReg(ODatamanager item)
        {
            var olddata = await _db.Students.Include(p => p.Guardian).ToListAsync();
            item.student.Guardian = olddata.FirstOrDefault().Guardian;
            item.student.Semester = await _db.Semesters.FindAsync(item.SemesterId);

            item.student.SchoolLevel = await _db.SchoolLevels.FindAsync(item.SchoolLevelId);
            if (item.student.Semester == null || item.student.SchoolLevel == null || item.student.SchoolFees == 0)
            {
                return View();


            }
            else
            {


                var feeslist = await _db.FeesPayments.ToListAsync();
                var studentPayDetails = (from trs in feeslist where trs.StudentId == item.student.StudentId select trs).ToList().LastOrDefault();


                transaction.Balance = item.student.SchoolFees + studentPayDetails.Balance;
                transaction.StudentLevel = await _db.SchoolLevels.FindAsync(item.SchoolLevelId);
                transaction.Semester = await _db.Semesters.FindAsync(item.SemesterId);
                transaction.StudentId = item.student.StudentId;

                transaction.Reason = "Student Registration";
                await _db.FeesPayments.AddAsync(transaction);
                await _db.SaveChangesAsync();
                await _db.UpdatePromotedStudentdata(item.student, _db);

                await _db.NewLevelData(item.student, _db);


                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> CreateCourse()
        {

            ODatamanager.schoolLevels = await _db.SchoolLevels.ToListAsync();
            ODatamanager.Semesters = await _db.Semesters.ToListAsync();
            return View(ODatamanager);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(ODatamanager item)
        {
            item.Course.SchoolLevel = await _db.SchoolLevels.FindAsync(item.SchoolLevelId);
            item.Course.Semester = await _db.Semesters.FindAsync(item.SemesterId);
            await _db.Courses.AddAsync(item.Course);
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");

        }
        public async Task<IActionResult> LevelCourses(int id)
        {
            await _db.CreateAssessedStudent(id, _db);
            var assessedStudents = await _db.AssessedStudents.Include(p => p.SchoolLevel).Include(k => k.Semester).ToListAsync();
            var asesedStudent = assessedStudents.FirstOrDefault();
            var Courses = await _db.Courses.Include(p => p.SchoolLevel).Include(k => k.Semester).Include(l => l.Assessement).ToListAsync();
            var AsesedCourses = Courses.Where(k => k.SchoolLevel.SchoolLevelId == asesedStudent.SchoolLevel.SchoolLevelId).ToList();
            ODatamanager.Courses = AsesedCourses;
            ODatamanager.student = await _db.Students.FindAsync(asesedStudent.StudentId);
            return View(ODatamanager);

        }

        public async Task<IActionResult> AddMarksToAssesment(int Id)
        {
            var Courses = await _db.Courses.Include(p => p.Assessement).Where(k => k.CourseId == Id).ToListAsync();

            var assessedStudents = await _db.AssessedStudents.Include(p => p.SchoolLevel).Include(k => k.Semester).ToListAsync();
            var asesedStudent = assessedStudents.FirstOrDefault();
            //look into studentAssessement


            var studentAssesemnts = await _db.StudentAssessemets.Include(k => k.Assessement).Where(p => p.AssessedStudentId == asesedStudent.StudentId).Select(p => p).Where(k => k.CourseId == Id).ToListAsync();
            var lastAssesement = studentAssesemnts.LastOrDefault();
            if (lastAssesement != null)
            {
                if (lastAssesement.AssessedSemesterId == asesedStudent.Semester.SemesterId && lastAssesement.AssessedLevelId == asesedStudent.SchoolLevel.SchoolLevelId && lastAssesement.Assessement.AssesedCourseId == Id)
                {
                    var assessedCourse = Courses.FirstOrDefault();
                    assessedCourse.Assessement = lastAssesement.Assessement;
                    ODatamanager.Course = assessedCourse;
                }
                else
                {
                    ODatamanager.Course = Courses.FirstOrDefault();

                }
            }
            else
            {
                ODatamanager.Course = Courses.FirstOrDefault();

            }
            var stdnt = await _db.Students.FindAsync(asesedStudent.StudentId);
            ODatamanager.student = stdnt;
            return View(ODatamanager);

        }
        [HttpPost]
        public async Task<IActionResult> AddMarksToAssesment(ODatamanager items)
        {
            var students = await _db.Students.Include(s => s.Semester).Where(k => k.StudentId == items.student.StudentId).ToListAsync();
            var asesedstudents = await _db.AssessedStudents.Include(p => p.SchoolLevel).Include(k => k.Semester).ToListAsync();
            var asesedstudent = asesedstudents.FirstOrDefault();


            //look into studentAssessement
            var studentAssesemnts = await _db.StudentAssessemets.Include(k => k.Assessement).Where(p => p.AssessedStudentId == asesedstudent.StudentId).Select(p => p).Where(k => k.CourseId == items.Course.CourseId).ToListAsync();
            var lastAssesement = studentAssesemnts.LastOrDefault();
            if (lastAssesement != null)
            {
                lastAssesement.Assessement.Cat1 = items.Course.Assessement.Cat1;
                lastAssesement.Assessement.Cat2 = items.Course.Assessement.Cat2;
                lastAssesement.Assessement.Final = items.Course.Assessement.Final;
                lastAssesement.Assessement.MidExam = items.Course.Assessement.MidExam;
                lastAssesement.Assessement.TotalScore = items.Course.Assessement.TotalScore;
                await _db.SaveChangesAsync();
            }
            else
            {
                var studentAssessment = new StudentAssessemet();
                studentAssessment.AssessedStudentId = asesedstudent.StudentId;
                studentAssessment.AssessedLevelId = asesedstudent.SchoolLevel.SchoolLevelId;
                studentAssessment.AssessedSemesterId = asesedstudent.Semester.SemesterId;
                studentAssessment.CourseId = items.Course.CourseId;
                items.Course.Assessement.AssesedCourseId = items.Course.CourseId;
                studentAssessment.Assessement = items.Course.Assessement;
                await _db.StudentAssessemets.AddAsync(studentAssessment);
                await _db.SaveChangesAsync();
            }





            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ReportCard(int id)
        {
            var Courses = await _db.Courses.Include(p => p.SchoolLevel).Include(k => k.Semester).Include(l => l.Assessement).ToListAsync();
            var students = await _db.Students.Include(p => p.SchoolLevel).Include(k => k.Semester).ToListAsync();
            ODatamanager.student = students.Where(p => p.StudentId == id).ToList().FirstOrDefault();
            ODatamanager.Courses = Courses.Where(k => k.SchoolLevel.SchoolLevelId == ODatamanager.student.SchoolLevel.SchoolLevelId).ToList();
            ODatamanager.studentId = ODatamanager.student.StudentId;

            return View(ODatamanager);

        }
        public async Task<IActionResult> ViewCourses()
        {
            var Courses = await _db.Courses.Include(p => p.SchoolLevel).ToListAsync();

            return View(Courses);
        }

        public async Task<IActionResult> StudentDetails(int Id)
        {
            var studentsList = await _db.Students.Where(k => k.StudentId == Id).Include(p => p.Guardian).ToListAsync();
            var studentData = await _db.LevelDatas.ToListAsync();
            var Levels = await _db.SchoolLevels.ToListAsync();
            var semesters = await _db.Semesters.ToListAsync();
            var student = (from stdnt in studentsList
                           join data in studentData on stdnt.StudentId equals data.StudentId
                           select new FeesJoinStudent
                           {
                               Guardian = stdnt.Guardian,
                               FirstName = stdnt.FirstName,
                               LastName = stdnt.LastName,
                               ContactS = stdnt.Contact,
                               ContactG = stdnt.Guardian.Contact,
                               DayB = stdnt.BirthDate.Day,
                               MonthB = stdnt.BirthDate.Month,
                               YearB = stdnt.BirthDate.Year,
                               Sex = stdnt.Sex,
                               Semester = semesters.Where(k => k.SemesterId == data.SemesterId).FirstOrDefault(),
                               SchoolLevel = Levels.Where(p => p.SchoolLevelId == data.SchoolLevelId).FirstOrDefault(),
                               Residance = stdnt.Residance,
                               StudentId = stdnt.StudentId,

                           }).ToList().FirstOrDefault();
            return View(student);
        }

        public async Task<IActionResult> FeesPaymentDetails(int Id)
        {
            var paymentdetails = await _db.FeesPayments.Include(p => p.Semester).Include(k => k.StudentLevel).Where(k => k.StudentId == Id).ToListAsync();
            var studentList = await _db.Students.Where(k => k.StudentId == Id).ToListAsync();
            //var Levels = await _db.SchoolLevels.ToListAsync();
            //var semesters = await _db.Semesters.ToListAsync();
            ODatamanager.FeesPayments = paymentdetails;
            ODatamanager.student = studentList.FirstOrDefault();


            return View(ODatamanager);
        }
        public async Task<IActionResult> LevelChangesLog(int Id)
        {
            var Levels = await _db.SchoolLevels.ToListAsync();
            var semesters = await _db.Semesters.ToListAsync();
            var levelChanges = await _db.LevelChanges.Include(k => k.Semester).Where(k => k.StudentId == Id).ToListAsync();
            var studentList = await _db.Students.Where(k => k.StudentId == Id).ToListAsync();

            var data = (from change in levelChanges
                        join item in studentList on change.StudentId equals item.StudentId
                        select new Student
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Sex = item.Sex,
                            SchoolLevel = change.SchoolLevel,
                            Semester = change.Semester,
                            SchoolFees = change.schoolFess,
                            RegistrationDate = item.RegistrationDate
                        }).ToList();
            ODatamanager.students = data;
            ODatamanager.student = studentList.FirstOrDefault();
            return View(ODatamanager);
        }
        public async Task<IActionResult> StudentReport(int Id)
        {
            var AssessmentList = await _db.StudentAssessemets.ToListAsync();
                var courses = await _db.Courses.ToListAsync();
            var assessements = await _db.Assessements.ToListAsync();
            var Levels = await _db.SchoolLevels.ToListAsync();
            var semesters = await _db.Semesters.ToListAsync();
            var studentList = await _db.Students.Include(k=>k.Semester).Include(k=>k.SchoolLevel).Where(k => k.StudentId == Id).ToListAsync();
            var results = (from entry in AssessmentList
                           join item in studentList on entry.AssessedStudentId equals item.StudentId
                           select new FeesJoinStudent
                           {  Course=courses.Where(k=>k.CourseId==entry.CourseId).FirstOrDefault(),
                               Assessement = assessements.Where(k=>k.AssessementId==entry.Assessement.AssessementId).FirstOrDefault(),
                               SchoolLevel = Levels.Where(k => k.SchoolLevelId == entry.AssessedLevelId).FirstOrDefault(),
                               Semester = semesters.Where(j => j.SemesterId == entry.AssessedSemesterId).FirstOrDefault(),
                           }).ToList();
            ODatamanager.FeesJoinStudents = results;
            ODatamanager.student = studentList.FirstOrDefault();
            return View(ODatamanager);
        
        }
      

        }
}
