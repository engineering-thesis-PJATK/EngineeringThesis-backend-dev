using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual Project PtkIdProjectNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
