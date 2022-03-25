using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
