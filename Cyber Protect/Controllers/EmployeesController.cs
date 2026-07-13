using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cyber_Protect.Data;
using Cyber_Protect.Models;

public class EmployeeController : Controller
{
    private readonly AppDbContext _context;
    public EmployeeController(AppDbContext context) => _context = context;

    // GET: Employee
    public async Task<IActionResult> Index()
    { 

        

        var employees = await _context.Employees.ToListAsync();
        return View(employees);
    }

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create() => View();

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployeeID,Analyst,Email")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Analyst,Email")] Employee employee)
    {
        if (id != employee.EmployeeID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeID == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null) _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}