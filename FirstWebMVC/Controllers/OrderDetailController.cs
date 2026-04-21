using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

public class OrderDetailController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderDetailController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = _context.OrderDetails
            .Include(o => o.Order)
            .Include(o => o.Product);

        return View(await data.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var orderDetail = await _context.OrderDetails
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(m => m.OrderDetailId == id);

        if (orderDetail == null) return NotFound();

        return View(orderDetail);
    }

    public IActionResult Create()
    {
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderDetail orderDetail)
    {
        if (ModelState.IsValid)
        {
            _context.Add(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderDetail);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var orderDetail = await _context.OrderDetails.FindAsync(id);
        if (orderDetail == null) return NotFound();

        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderDetail.ProductId);

        return View(orderDetail);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, OrderDetail orderDetail)
    {
        if (id != orderDetail.OrderDetailId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(orderDetail);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var orderDetail = await _context.OrderDetails
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(m => m.OrderDetailId == id);

        if (orderDetail == null) return NotFound();

        return View(orderDetail);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);
        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}