
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cyber_Protect.Models;
using Cyber_Protect.Data;

public class ThreatsController : Controller
{
    private readonly AppDbContext _context;

    public ThreatsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: THREATS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Threats.ToListAsync());
    }

    // GET: THREATS/Details/5
    public async Task<IActionResult> Details(int? threatid)
    {
        if (threatid == null)
        {
            return NotFound();
        }

        var threat = await _context.Threats
            .FirstOrDefaultAsync(m => m.ThreatID == threatid);
        if (threat == null)
        {
            return NotFound();
        }

        return View(threat);
    }

    // GET: THREATS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: THREATS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ThreatID,level,Severity,Incidents")] Threat threat)
    {
        if (ModelState.IsValid)
        {
            _context.Add(threat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(threat);
    }

    // GET: THREATS/Edit/5
    public async Task<IActionResult> Edit(int? threatid)
    {
        if (threatid == null)
        {
            return NotFound();
        }

        var threat = await _context.Threats.FindAsync(threatid);
        if (threat == null)
        {
            return NotFound();
        }
        return View(threat);
    }

    // POST: THREATS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? threatid, [Bind("ThreatID,level,Severity,Incidents")] Threat threat)
    {
        if (threatid != threat.ThreatID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(threat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreatExists(threat.ThreatID))
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
        return View(threat);
    }

    // GET: THREATS/Delete/5
    public async Task<IActionResult> Delete(int? threatid)
    {
        if (threatid == null)
        {
            return NotFound();
        }

        var threat = await _context.Threats
            .FirstOrDefaultAsync(m => m.ThreatID == threatid);
        if (threat == null)
        {
            return NotFound();
        }

        return View(threat);
    }

    // POST: THREATS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? threatid)
    {
        var threat = await _context.Threats.FindAsync(threatid);
        if (threat != null)
        {
            _context.Threats.Remove(threat);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ThreatExists(int? threatid)
    {
        return _context.Threats.Any(e => e.ThreatID == threatid);
    }
}
