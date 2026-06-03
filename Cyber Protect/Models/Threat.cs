using System.ComponentModel.DataAnnotations;

namespace Cyber_Protect.Models
{
    public class Threat
    {
        [Key]
        public int ThreatID { get; set; }

        [Required]
        public int level { get; set; }

        [Required , StringLength(20)]
        public string Severity { get; set; }


    }
}
