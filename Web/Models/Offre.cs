//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobsForCoders.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Offre
    {
        public Offre()
        {
            this.Applications = new HashSet<Application>();
        }
    
        public int JobID { get; set; }
        public int EntrepriseID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Buzz_Words { get; set; }
        public double Salary { get; set; }
    
        public virtual Entreprise Entreprise { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}