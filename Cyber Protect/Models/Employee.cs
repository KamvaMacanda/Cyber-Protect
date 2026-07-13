using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cyber_Protect.Models
{
    public class Employee
    { 
        [Key]
        public int EmployeeID { get; set; }

        [Required, StringLength (20)]
        public string Analyst { get; set; } = string.Empty;

        [Required , EmailAddress]
         public string ? Email { get; set; }

        [NotMapped]
        public int Count { get; set; }

        public ICollection <Incident> Incidents { get; set; } = new List<Incident>();


    }
}
