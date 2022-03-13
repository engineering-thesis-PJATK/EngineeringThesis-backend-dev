using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeeTeam
    {
        public int EtmId { get; set; }
        public int EtmIdEmployee { get; set; }
        public int EtmIdTeam { get; set; }
        public int EtmIdRole { get; set; }

        public virtual Employee EtmIdEmployeeNavigation { get; set; }
        public virtual Role EtmIdRoleNavigation { get; set; }
        public virtual Team EtmIdTeamNavigation { get; set; }
    }
}
