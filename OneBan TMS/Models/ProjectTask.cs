using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            TimeEntries = new HashSet<TimeEntry>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PtkId { get; set; }
        public string PtkContent { get; set; }
        public decimal PtkEstimatedCost { get; set; }
        public int PtkIdProject { get; set; }

        public virtual Project PtkIdProjectNavigation { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
