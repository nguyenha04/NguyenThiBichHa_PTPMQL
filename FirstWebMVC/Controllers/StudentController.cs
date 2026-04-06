using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstWebMVC.Controllers
{
    public class StudentController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // READ
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        // CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }

            var exists = await _context.Students
                .AnyAsync(s => s.StudentCode == std.StudentCode);

            if (exists)
            {
                ModelState.AddModelError("", "Mã sinh viên đã tồn tại!");
                return View(std);
            }

            _context.Students.Add(std);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return View("NotFound");
            }

            return View(student);
        }

        // EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }

            _context.Students.Update(std);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return View("NotFound");
            }

            return View(student);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}