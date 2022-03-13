using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Role
    {
        public Role()
        {
            EmployeeTeams = new HashSet<EmployeeTeam>();
        }

        public int RolId { get; set; }
        public string RolDescription { get; set; }
        public string RolName { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}
