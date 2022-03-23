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
        public string PtkContent { get; set; }
        public decimal PtkEstimatedCost { get; set; }
        public int PtkIdProject { get; set; }

        public virtual Project PtkIdProjectNavigation { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
