using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeeTeamRole
    {
        public EmployeeTeamRole()
        {
            EmployeeTeams = new HashSet<EmployeeTeam>();
        }

        public int EtrId { get; set; }
        public string EtrName { get; set; }
        public string EtrDescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}
