using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            TimeEntries = new HashSet<TimeEntry>();
        }

        public int PtkId { get; set; }
        public decimal PtkEstCost { get; set; }
        public string PtkContent { get; set; }
        public int PtkProjectId { get; set; }

        public virtual Project PtkProject { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
