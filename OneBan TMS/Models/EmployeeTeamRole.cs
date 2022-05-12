using System;
using System.Collections.Generic;

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

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}
