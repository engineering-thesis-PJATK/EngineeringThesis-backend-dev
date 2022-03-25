using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyNotes = new HashSet<CompanyNote>();
            Customers = new HashSet<Customer>();
            Projects = new HashSet<Project>();
        }

        public int CmpId { get; set; }
        public string CmpName { get; set; }
        public string CmpNip { get; set; }
        public string CmpNipPrefix { get; set; }
        public string CmpRegon { get; set; }
        public string CmpKrsNumber { get; set; }
        public string CmpLandline { get; set; }
        
        public int CmpIdAdress { get; set; }
        [JsonIgnore]
        public virtual Address CmpIdAdressNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<CompanyNote> CompanyNotes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Customer> Customers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
