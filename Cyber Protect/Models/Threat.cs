using System.ComponentModel.DataAnnotations;

namespace Cyber_Protect.Models
{
    public class Threat
    {
        [Key]
        public int ThreatID { get; set; }

        [Required , StringLength(20)]
        public string level { get; set; }

        [Required , StringLength(20)]
        public string Severity { get; set; }
           

        // wriite a report about threat
        [Required]
        public string Report { get; set; } = string.Empty; 

        public int ThreatScore { get; set; }

      


        public DateTime DateLogged { get; set; } = DateTime.Now;

        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
