using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = _context.Orders.Include(o => o.Customer);
        return View(await orders.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(m => m.OrderId == id);

        if (order == null) return NotFound();

        return View(order);
    }

    public IActionResult Create()
    {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Order order)
    {
        if (id != order.OrderId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var order = await _context.Orders
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(m => m.OrderId == id);

        if (order == null) return NotFound();

        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}