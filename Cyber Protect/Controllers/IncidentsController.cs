using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cyber_Protect.Data;
using System.IO;
using Cyber_Protect.Models;

namespace Cyber_Protect.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly AppDbContext _context;

        public IncidentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Incidents
        public async Task<IActionResult> Index()
        { 
            //load all incidents and threats , and reports 
            var incidents = await _context.Incidents
                .Include(i => i.Threat)
                .Include(i => i.Employee)
                .ToListAsync(); 
            
            
            return View(incidents);
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .FirstOrDefaultAsync(m => m.IncidentID == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // GET: Incidents/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.CompanySystems = CompanySystems;
            await CreateDropDown();
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentID,Title,Description,Status,DateReported,ThreatID,EmployeeID,AddNotes")] Incident incident)
        {
            // collect checkbox values (multiple) and store as a comma-separated string
            var selectedSystems = Request.Form["AffectedSystems"].ToArray();
            incident.AffectedSystems = (selectedSystems != null && selectedSystems.Length > 0)
                ? string.Join(", ", selectedSystems)
                : string.Empty;

            if (ModelState.IsValid)
            {
                _context.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CompanySystems = CompanySystems;
            await CreateDropDown(incident.ThreatID, incident.EmployeeID);
            return View(incident);
        }

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            return View(incident);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidentID,Title,Description,Status,DateReported")] Incident incident)
        {
            if (id != incident.IncidentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.IncidentID))
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

            await CreateDropDown(incident.ThreatID, incident.EmployeeID);
            return View(incident);
        }

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .FirstOrDefaultAsync(m => m.IncidentID == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident != null)
            {
                _context.Incidents.Remove(incident);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentID == id);
        } 

        // Creating a dropdown for  threats to be choosen for report  
         public async Task CreateDropDown (int ? selectedThreat = null , int? selectedEmployee = null )
        { 

            ViewBag.EmployeeID = new SelectList(
                await _context.Employees
                .AsNoTracking()
                .ToListAsync(),
              "EmployeeID", "Analyst", selectedEmployee
            );



            ViewBag.ThreatID = new SelectList(
                await _context.Threats
                .AsNoTracking()
                .ToListAsync(), 
              "ThreatID", "Report", selectedThreat
            );
        }



        // creating a list of all systems in the company 

        private static readonly List<string> CompanySystems = new()
        {
            "Server", 
            "WiFi", 
            "Network",
            "Firewall", 
            "Database", 
            "Workstation", 
            "VPN", 
            "Email Server" 

        };


        public async Task<IActionResult> Filter(string status)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Threat)
                .Include(i => i.Employee)
                .Where(i => string.IsNullOrEmpty(status) || i.Status == status)
                .ToListAsync();

            return View("Index", incidents);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(int? id)
        {
         
            if (id == null)
            {
                return NotFound();
            }

     
            var threat = await _context.Threats
                .Include(t => t.Incidents)
                .FirstOrDefaultAsync(t => t.ThreatID == id);


            var incidents = await _context.Incidents
                .Include(i => i.Employee)
                   .FirstOrDefaultAsync(i => i.EmployeeID == id);

            if (threat == null)
            {
                return NotFound();
            }

            // Build the report text
            string report = $"Threat Report:\n" +
                $"Threat ID: {threat.ThreatID}\n" +
                $"Level: {threat.level}\n" + 
                $"Threat: {threat.Report}\n" +
                $"Logged By: {incidents.Employee}\n" +
                $"Severity: {threat.Severity}\n" + 
                 $"Incidents: {threat.Incidents?.Count ?? 0}\n" +
                $"Threat percentage Score: {threat.ThreatScore}%";

           
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Threat Assessment Reports");
            Directory.CreateDirectory(folder);

            // Save the report as a text file named after the threat ID
            string fileName = $"Threat-{threat.ThreatID}.txt";
            string filePath = Path.Combine(folder, fileName);

            // Still write a server-side copy, same as before
            await System.IO.File.WriteAllTextAsync(filePath, report);

            // CHANGED: instead of returning a View, convert the report to bytes
            // and return it via ControllerBase.File(...), which sends the file
            // as the HTTP response body with headers that tell the browser to
            // download it rather than display it.
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(report);
            return File(fileBytes, "text/plain", fileName);


        }


    }
}
