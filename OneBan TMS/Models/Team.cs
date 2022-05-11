using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Team
    {
        public Team()
        {
            EmployeeTeams = new HashSet<EmployeeTeam>();
            Projects = new HashSet<Project>();
        }

        public int TemId { get; set; }
        public string TemName { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
