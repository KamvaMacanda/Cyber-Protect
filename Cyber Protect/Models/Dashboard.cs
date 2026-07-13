namespace Cyber_Protect.Models
{
    public class Dashboard
    {

        // To display the total number of incidents, threats and employeess 
        //for management  
        public int OpenIncidents { get; set; }
        public int ClosedThisMonth { get; set; }
        public int CriticalThreats { get; set; }
        

        
        public List<Threat> ThreatsBySeverity { get; set; } = new();

        // Display thr analyhst  ame and incident totoal in pie chart (chart.js) 
        public List<Employee> IncidentsByAnalyst { get; set; } = new();


    }


}
