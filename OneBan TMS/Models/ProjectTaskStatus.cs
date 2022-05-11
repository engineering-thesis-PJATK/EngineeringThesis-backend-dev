using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class ProjectTaskStatus
    {
        public ProjectTaskStatus()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public int PtsId { get; set; }
        public string PtsName { get; set; }
        public string PtsDescription { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
