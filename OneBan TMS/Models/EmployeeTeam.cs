using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeeTeam
    {
        public EmployeeTeam()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int EtmId { get; set; }
        public int EtmIdEmployee { get; set; }
        public int EtmIdTeam { get; set; }
        public int EtmIdRole { get; set; }
        [JsonIgnore]
        public virtual Employee EtmIdEmployeeNavigation { get; set; }
        [JsonIgnore]
        public virtual EmployeeTeamRole EtmIdRoleNavigation { get; set; }
        [JsonIgnore]
        public virtual Team EtmIdTeamNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
