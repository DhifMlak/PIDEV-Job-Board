using System;
using System.Collections.Generic;

namespace JobsForCoders.Models
{
    public class Report
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string EmployerName { get; set; }
        public string JobSeekerName { get; set; }
        public string Position { get; set; }
        public string ExportTo { get; set; }
        public Candidat JobSeeker { get; set; }
        public Entreprise Entreprise { get; set; }
        public List<Application> Applications { get; set; }
    }
}