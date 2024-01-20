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
            return RedirectToAction("Index");        }
    }
}
