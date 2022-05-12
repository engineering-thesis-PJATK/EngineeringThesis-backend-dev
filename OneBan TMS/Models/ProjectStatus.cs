using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            Projects = new HashSet<Project>();
        }

        public int PjsId { get; set; }
        public string PjsName { get; set; }
        public string PjsDescription { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
