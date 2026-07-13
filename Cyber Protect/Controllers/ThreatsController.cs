using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cyber_Protect.Models;
using Cyber_Protect.Data;

namespace Cyber_Protect.Controllers
{
    public class ThreatController : Controller
    {
        private readonly AppDbContext _context;

        public ThreatController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Threat
        public async Task<IActionResult> Index()
        {
            var threats = _context.Threats  
                        .Include(t => t.ThreatScore)
                        .Include(t => t.level)
                        .Include(t => t.LoggedBy) 
                        .Include(t => t.Severity)
                         .AsNoTracking();





            var model = await _context.Threats.ToListAsync();
            return View(model);
        }

        // GET: Threat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var threat = await _context.Threats.FirstOrDefaultAsync(m => m.ThreatID == id);
            if (threat == null) return NotFound();

            return View(threat);
        }

        // GET: Threat/Create
        public IActionResult Create() => View(new Threat());

        // POST: Threat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreatID,level,Severity,Report,ThreatScore,LoggedBy")] Threat threat)
        {
            Dictionary<string, int> keywordScores = new()
            {
                { "malware", 10 }, { "phishing", 20 }, { "suspicious activity", 35},
                { "ransomware", 20 }, { "data breach", 20}, { "vulnerability", 59 },
                { "insider threat", 60 }, { "social engineering", 60 }, { "botnet", 55 }
            };

            string userInput = (threat.Report ?? string.Empty).ToLower();
            int score = 0;
            foreach (var keyword in keywordScores)
                if (userInput.Contains(keyword.Key)) score += keyword.Value;

            threat.ThreatScore = score;
            threat.Severity = GetRiskCategory(score);

            if (ModelState.IsValid)
            {
                _context.Add(threat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threat);
        }

        // GET: Threat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var threat = await _context.Threats.FindAsync(id);
            if (threat == null) return NotFound();
            return View(threat);
        }

        // POST: Threat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ThreatID,level,Severity,Report,ThreatScore,LoggedBy,Incidents")] Threat threat)
        {
            if (id != threat.ThreatID) return NotFound();

            var existing = await _context.Threats.AsNoTracking().FirstOrDefaultAsync(t => t.ThreatID == id);
            if (existing == null) return NotFound();
            threat.DateLogged = existing.DateLogged;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(threat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreatExists(threat.ThreatID)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(threat);
        }

        // GET: Threat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var threat = await _context.Threats.FirstOrDefaultAsync(m => m.ThreatID == id);
            if (threat == null) return NotFound();

            return View(threat);
        }

        // POST: Threat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var threat = await _context.Threats.FindAsync(id);
            if (threat != null) _context.Threats.Remove(threat);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreatExists(int? id) => _context.Threats.Any(e => e.ThreatID == id);

       

        private string GetRiskCategory(int score)
        {
            if (score >= 80) return "Critical";
            if (score >= 50) return "High";
            if (score >= 25) return "Medium";
            return "Low";
        } 


      


    }
}