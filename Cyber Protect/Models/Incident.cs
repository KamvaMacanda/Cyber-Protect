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

        [DataType(DataType.Date)]
        public DateTime DateReported { get; set; } 
         
        public string Assign { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string AddNotes { get; set; } = string.Empty;


        //Navigateion Properties using FKs 

        [Display(Name = "Analyst")]
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

        [Display (Name ="Threat Assemnet ")]
        public int ThreatID { get; set; }
        public Threat? Threat { get; set; }



    }  
}
