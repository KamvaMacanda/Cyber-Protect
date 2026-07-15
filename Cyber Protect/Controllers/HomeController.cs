using Cyber_Protect.Data;
using Cyber_Protect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cyber_Protect.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;


        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        //passes the Dashboard model
        public async Task<IActionResult> Index()
        {
            var dashboard = new Dashboard
            {
                OpenIncidents = await _context.Incidents
                    .CountAsync(i => i.Status == "Open"),

                ClosedThisMonth = await _context.Incidents
                    .CountAsync(i => i.Status == "Resolved"
                               && i.DateReported.Month == DateTime.Now.Month
                               && i.DateReported.Year == DateTime.Now.Year),

                CriticalThreats = await _context.Threats
                    .CountAsync(t => t.Severity == "Critical"),

                IncidentsByAnalyst = await _context.Employees
                    .Select(e => new Employee
                    {
                        EmployeeID = e.EmployeeID,
                        Analyst = e.Analyst,
                        Count = e.Incidents.Count()
                    })
                    .ToListAsync(),

                ThreatsBySeverity = await _context.Threats.ToListAsync()
            };

            return View(dashboard);
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
