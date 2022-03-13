using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int ProId { get; set; }
        public string ProName { get; set; }
        public int ProIdCompany { get; set; }
        public string ProDescription { get; set; }
        public DateTime ProCreationDate { get; set; }
        public DateTime? ProCompletionDate { get; set; }
        public int ProIdTeam { get; set; }

        public virtual Company ProIdCompanyNavigation { get; set; }
        public virtual Team ProIdTeamNavigation { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
