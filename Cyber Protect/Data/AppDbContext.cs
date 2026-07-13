using Cyber_Protect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cyber_Protect.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Threat> Threats { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
