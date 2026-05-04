using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;
using FirstWebMVC.Models.ViewModels;
using System.ComponentModel.Design;
using FirstWebMVC.Models.Process;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
            _excelProcess = new ExcelProcess(); 
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students
                            .Select(s => new StudentVM
                            {
                                StudentCode = s.StudentCode,
                                FullName = s.FullName,
                                FacultyName = s.Faculty!.FacultyName
                            })
                            .ToListAsync();
            return View(result);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,FullName,FacultyId")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (StudentExists(student.StudentCode))
                {
                    ModelState.AddModelError("StudentCode", "Ma sinh vien da ton tai");
                    return View(student);
                }
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentCode,FullName,FacultyId")] Student student)
        {
            if (id != student.StudentCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentCode == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentCode == id);
        }
        // GET
    public IActionResult Upload()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Vui lòng chọn file Excel");
            return View();
        }

        // 1. Check định dạng file
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (extension != ".xls" && extension != ".xlsx")
        {
            ModelState.AddModelError("", "Chỉ chấp nhận file Excel");
            return View();
        }

        // 2. Tạo folder
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Excels");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // 3. Tạo tên file an toàn
        var fileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadPath, fileName);

        // 4. Lưu file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // 5. Đọc Excel → DataTable
        DataTable dt = _excelProcess.ExcelToDataTable(filePath);

        // Lấy tên cột
        List<string> columns = new List<string>();

        foreach (DataColumn col in dt.Columns)
        {
            columns.Add(col.ColumnName);
        }

        ViewBag.Columns = columns;
        // 6. Map DataTable → Student
foreach (DataRow row in dt.Rows)
{
   string studentCode = dt.Columns.Contains("StudentCode")
    ? row["StudentCode"]?.ToString()?.Trim()
    : row[0]?.ToString()?.Trim();

string fullName = dt.Columns.Contains("FullName")
    ? row["FullName"]?.ToString()?.Trim()
    : row[1]?.ToString()?.Trim();

string facultyName = dt.Columns.Contains("FacultyName")
    ? row["FacultyName"]?.ToString()?.Trim()
    : row[2]?.ToString()?.Trim();

    if (string.IsNullOrEmpty(studentCode) || string.IsNullOrEmpty(facultyName))
    {
        continue;
    }

    var faculty = _context.Faculties
        .FirstOrDefault(f => f.FacultyName.ToLower() == facultyName.ToLower());

    if (faculty == null)
    {
        continue;
    }

    if (StudentExists(studentCode))
    {
        continue;
    }

    var student = new Student
    {
        StudentCode = studentCode,
        FullName = fullName,
        FacultyId = faculty.FacultyId
    };

    _context.Students.Add(student);
}


        // 7. Lưu DB
        await _context.SaveChangesAsync();

        ViewBag.Message = "Import thành công!";
        return RedirectToAction("Index");
        }
        
    }
}

    




