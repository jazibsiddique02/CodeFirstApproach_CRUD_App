using CodeFirstASPCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstASPCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(StudentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }

        public async Task<IActionResult> Index()
        {
            var std = await studentDB.Students.ToListAsync();
            return View(std);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var std = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var std = await studentDB.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var std = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (std == null)
            {
                return NotFound();
            }

            return View(std);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            
            var std = await studentDB.Students.FindAsync(id);
            if (std != null)
            {
                studentDB.Students.Remove(std);
            }

            await studentDB.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDB.Students.AddAsync(std);
                await studentDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDB.Update(std);
                await studentDB.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(std);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
