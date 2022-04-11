using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        
        [JsonIgnore]
        public virtual Company ProIdCompanyNavigation { get; set; }
        [JsonIgnore]
        public virtual Team ProIdTeamNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
