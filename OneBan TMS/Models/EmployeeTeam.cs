using System;
using System.Collections.Generic;
<<<<<<< .merge_file_LZCR93
=======
using System.Text.Json.Serialization;
>>>>>>> .merge_file_6NoD0D

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
<<<<<<< .merge_file_LZCR93

        public virtual Employee EtmIdEmployeeNavigation { get; set; }
        public virtual EmployeeTeamRole EtmIdRoleNavigation { get; set; }
        public virtual Team EtmIdTeamNavigation { get; set; }
=======
        [JsonIgnore]
        public virtual Employee EtmIdEmployeeNavigation { get; set; }
        [JsonIgnore]
        public virtual EmployeeTeamRole EtmIdRoleNavigation { get; set; }
        [JsonIgnore]
        public virtual Team EtmIdTeamNavigation { get; set; }
        [JsonIgnore]
>>>>>>> .merge_file_6NoD0D
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
