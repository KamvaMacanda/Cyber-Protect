using System.ComponentModel.DataAnnotations;

namespace Cyber_Protect.Models
{
    public class Employee
    { 
        [Key]
        public int EmployeeID { get; set; }

        [Required, StringLength (20)]
        public string ? FullName { get; set; }

        [Required , EmailAddress]
         public string ? Email { get; set; } 
    
         public ICollection <Incident> Incidents { get; set; }
    

    }
}
