using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using schoolApp.Models;
using schoolApp.Models.Context;

namespace schoolApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ICollection<Course> Courses { get; set; }=new List<Course>();
        public ICollection<SchoolLevel> SchoolLevels { get; set; } = new List<SchoolLevel>();
        public ICollection<Staff> StaffList { get; set; }

        public Payment payment = new Payment();

        public StaffController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {

            StaffList = await _db.Staffs.ToListAsync();
            
            return View(StaffList);
        }

        public async Task<IActionResult> Create()
        {
            Staff Staff = new Staff();

            return View(Staff);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                await _db.Staffs.AddAsync(staff);
                await _db.SaveChangesAsync();
            }
            else
            {
                return View();

            }
            return RedirectToAction("Index");
                
                }

        public async Task<IActionResult> Payment (int Id)
        {
            var staff = await _db.Staffs.Where(k=>k.StaffId==Id).ToListAsync();
            var staffLegder = await  _db.Payments.Where(p=>p.StaffId==Id).ToListAsync();
            payment.Balance = staffLegder.LastOrDefault().Balance;
            payment.StaffId = staff.FirstOrDefault().StaffId;
            payment.Staff = staff.FirstOrDefault();
            return View(payment);

        }
        [HttpPost]
        public async Task<IActionResult> Payment(Payment payment)
        {
            var transaction = new GeneralLedger();
            var lastTransaction = await _db.GeneralLedger.ToListAsync();

            var staff = await _db.Staffs.Where(k => k.StaffId == payment.StaffId).ToListAsync();
            payment.Staff = staff.FirstOrDefault();
            var item = lastTransaction.LastOrDefault();
                payment.BalanceCalculator(staff.FirstOrDefault(),payment.Amount,payment.Balance);
                await _db.Payments.AddAsync(payment);
            transaction.Amount = payment.Amount;
            transaction.Reason = payment.Reason;
            if (item!=null) {
                transaction.TotalExpenses = item.TotalExpenses+payment.Amount;
            }
            else
            {
                transaction.TotalExpenses = payment.Amount;
            }
            await _db.GeneralLedger.AddAsync(transaction);
                await _db.SaveChangesAsync();

            
            return RedirectToAction("Index");

        }
    }
}
