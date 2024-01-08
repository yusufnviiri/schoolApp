using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolApp.Models;
using schoolApp.Models.Context;

namespace schoolApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ICollection<Student> Students { get; set; }
        public Student StudentO { get; set; }
        public LevelChange LevelChange { get; set; } = new LevelChange();
        public LevelData LevelData { get; set; } = new LevelData();
        public FeesPayment transaction { get; set; }= new FeesPayment();
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public ODatamanager ODatamanager { get; set; } = new ODatamanager();

        //index
        public async Task<IActionResult> Index()
        {
            Students = await _db.Students.Include(p => p.SchoolLevel).ToListAsync();

            return View(Students);
        }
        //create a student get
        public async Task<IActionResult> Create()
        {
            ODatamanager.schoolLevels = await _db.SchoolLevels.ToListAsync();
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
            collection.student.SchoolLevel = await _db.SchoolLevels.FindAsync(collection.student.LevelId);
            await _db.Students.AddAsync(collection.student);
            await _db.SaveChangesAsync();
            Students = await _db.Students.ToListAsync();
            var student = Students.LastOrDefault();
            // add stdent Data to Level data
             LevelData = new LevelData();
            LevelData.Student = student;

            await _db.LevelDatas.AddAsync(LevelData);
            await _db.SaveChangesAsync();
            //create a level Change
           
            _db.NewLevelData(student,_db);
            //create student ledger
            _db.NewStudentFeesLedger(student, _db);
            

           

            return RedirectToAction("Index");
        }
        //       pay school fees get

        public async Task<IActionResult> PayFees(int Id)
        {
            var transactions = await _db.FeesPayments.ToListAsync();
            var lastData = await _db.LevelChanges.ToListAsync();

            var students = await _db.Students.Include(t=>t.SchoolLevel).ToListAsync();
            var transact = (from std in students
                        join tr in transactions on std.StudentId equals tr.StudentId
                        select new FeesJoinStudent
                        {
                            FirstName = std.FirstName,
                            LastName = std.LastName,
                            Semester=lastData.LastOrDefault().Semester,
                            Sex=std.Sex,
                            Balance = tr.Balance,
                            PrevBalance = tr.PrevBalance,
                            SchoolLevel = tr.StudentLevel,
                            SchoolFees = lastData.LastOrDefault().schoolFess,
                            StudentId = std.StudentId,

                        }).ToList().LastOrDefault();
            var transaction = (from trans in transactions where trans.StudentId == Id select trans).LastOrDefault();
            return View(transact);

        }
        //       pay school fees post
        [HttpPost]
        public async Task<IActionResult> PayFees(FeesJoinStudent transaction)
        {// compute fees balaces
            var student = await _db.Students.FindAsync(transaction.StudentId);
            var newTrans = new FeesPayment();
            newTrans.StudentId = student.StudentId;
            newTrans.Semester = transaction.Semester;
            newTrans.PrevBalance = transaction.Balance;
            newTrans.Balance = transaction.Balance - transaction.Amount;
            newTrans.RecievedBy = transaction.RecievedBy;
            newTrans.Amount = transaction.Amount;
            newTrans.StudentLevel = transaction.SchoolLevel;
            newTrans.Reason = transaction.Reason;
            await _db.FeesPayments.AddAsync(newTrans);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // register new semester get
        public async Task<IActionResult> NewSemesterReg(int Id)
        {
            var AllStudents = await _db.Students.Include(p => p.SchoolLevel).ToListAsync();
            StudentO = (from std in AllStudents where std.StudentId == Id select std).FirstOrDefault();
            StudentO.Semester = string.Empty;
            StudentO.SchoolFees = 0;
            StudentO.SchoolLevel = null;
            ODatamanager.student = StudentO;
            var levels = await _db.SchoolLevels.ToListAsync();
            ODatamanager.schoolLevels =levels ;
            return View(ODatamanager);
        }
        // register new semester post

        [HttpPost]
        public async Task<IActionResult> NewSemesterReg(ODatamanager item)
        {
            var olddata = await _db.Students.Include(p=>p.Guardian).ToListAsync();
           item.student.Guardian = olddata.FirstOrDefault().Guardian;

            item.student.SchoolLevel = await _db.SchoolLevels.FindAsync(item.student.LevelId);
            if (item.student.Semester == null ||item. student.SchoolLevel == null || item.student.SchoolFees == 0)
            {
                return View();

             
            }
            else
            {
                //Add levelData
                //  LevelData = new LevelData();
                //  LevelData.Student = item.student; 
                //await  _db.LevelDatas.AddAsync(LevelData);
                //  await _db.SaveChangesAsync();

                // update student ledger

                // var studentPayDetails = await _db.FeesPayments.Where(p=>p.StudentId==item.student.StudentId).OrderBy(k=>k.StudentId).FirstOrDefaultAsync();

                var feeslist = await _db.FeesPayments.ToListAsync();
                var studentPayDetails = (from trs in feeslist where trs.StudentId == item.student.StudentId select trs).ToList().LastOrDefault();


                transaction.Balance = item.student.SchoolFees +studentPayDetails.Balance;
                transaction.StudentLevel = item.student.SchoolLevel.LevelName;
                transaction.Semester = item.student.Semester;
                transaction.StudentId = item.student.StudentId;
                
                transaction.Reason = "Student Registration";
                await _db.FeesPayments.AddAsync(transaction);
                await _db.SaveChangesAsync();
                _db.NewLevelData(item.student, _db);


                return RedirectToAction("Index");
            }

        }
    }
    }
