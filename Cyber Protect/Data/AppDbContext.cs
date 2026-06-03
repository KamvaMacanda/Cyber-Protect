using Cyber_Protect.Models;
using Microsoft.EntityFrameworkCore;

namespace Cyber_Protect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Threat> Threats { get; set; }

        public DbSet<Employee> Employees { get; set; }

        
    }
}
