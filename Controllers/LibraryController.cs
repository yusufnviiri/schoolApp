using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolApp.Models;
using schoolApp.Models.Context;

namespace schoolApp.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<LibItem> LibItems { get; set; }
        public LibraryController(ApplicationDbContext db)
        {
                _db = db;
        }
        public async Task<IActionResult> Index()
        {
            LibItems = await _db.LibItems.ToListAsync();
            
            return View(LibItems);
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( LibItem libItem)
        {
            if (ModelState.IsValid)
            {
                await _db.LibItems.AddAsync(libItem);
                await _db.SaveChangesAsync();
            }
            //return RedirectToAction("Index");  
            return new RedirectToActionResult("Index", "Student",null);
        }
        public async Task<IActionResult> BorrowItem(int Id)
        {
            var users = await _db.Users.ToListAsync();
            if (users.Count > 0)
            {
                _db.Remove(users.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            var user = new User();
            var student = await _db.Students.FindAsync(Id);
            user.StudentId = student.StudentId;
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> BorrowingDetails(int Id)
        { var libItem = await _db.LibItems.FindAsync(Id);
            var users = await _db.Users.ToListAsync();
            if (users.Count() < 1)
            {
                return RedirectToAction("Create");
            }
            else
            {
                var libUser = new LibUser();
                libUser.StudentId = users.FirstOrDefault().StudentId;
                libUser.LibItemId = libItem.LibItemId;
                libUser.ReturnDate = DateTime.UtcNow;
                libUser.Quantity = 1;
                await _db.LibUsers.AddAsync(libUser);
                libItem.Quantity = libItem.Quantity - 1;
                await _db.SaveChangesAsync();
            }
            return new RedirectToActionResult("Index", "Student", null);
        }
        public async Task<IActionResult> LibUsers()
        {
            var libItems = await _db.LibItems.ToListAsync();
            var students = await _db.Students.ToListAsync();
            var libUsers = await _db.LibUsers.ToListAsync();

            var data = (from item in libUsers
                        select new FeesJoinStudent
                        {
                            student = students.Where(k => k.StudentId == item.StudentId).FirstOrDefault(),
                            libItem = libItems.Where(k => k.LibItemId == item.LibItemId).FirstOrDefault(),
                            Quantity = item.Quantity,
                            Date = item.ReturnDate,

                        }).ToList();



            return View(data);
        }
    }
}
;