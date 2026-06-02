using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Cyber_Protect.Models
{
    public class Incident
    {
        [Key] 
        public int IncidentID { get; set; }

        [StringLength(60)]
        public string AffectedSystems { get; set; }  


        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [StringLength(10)]
        public  string Status { get; set; }

        [Required, Timestamp]
        public DateTime DateReported { get; set; } 
         
        public string Assign { get; set; } = string.Empty;


        public   ICollection <Employee> Employees { get; set; } 
        public ICollection <Threat> Threats { get; set; }

    }  
}
