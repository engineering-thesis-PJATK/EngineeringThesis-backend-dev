using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProId { get; set; }
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public DateTime ProCreatedAt { get; set; }
        public DateTime? ProCompletedAt { get; set; }
        public int ProIdCompany { get; set; }
        public int ProIdTeam { get; set; }

        public virtual Company ProIdCompanyNavigation { get; set; }
        public virtual Team ProIdTeamNavigation { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
