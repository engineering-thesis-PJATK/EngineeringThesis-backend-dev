using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OneBan_TMS.Models.DTOs.Project;

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
        public string ProDescription { get; set; }
        public DateTime ProCreatedAt { get; set; }
        public DateTime? ProCompletedAt { get; set; }
        public int ProIdCompany { get; set; }
        public int ProIdTeam { get; set; }
        public int ProIdProjectStatus { get; set; }
        [JsonIgnore]
        public virtual Company ProIdCompanyNavigation { get; set; }
        [JsonIgnore]
        public virtual ProjectStatus ProIdProjectStatusNavigation { get; set; }
        [JsonIgnore]
        public virtual Team ProIdTeamNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }

        public ProjectCompanyTeamNamesDto GetProjectCompanyTeamNamesDto()
        {
            return new ProjectCompanyTeamNamesDto()
            {
                ProId = this.ProId,
                ProName = this.ProName,
                ProDescription = this.ProDescription,
                ProCreatedAt = this.ProCreatedAt,
                ProCompletedAt = this.ProCompletedAt,
                ProIdCompany = this.ProIdCompany,
                ProIdTeam = this.ProIdTeam,
                ProIdProjectStatus = this.ProIdProjectStatus,
                ProCompanyName = this.ProIdCompanyNavigation.CmpName,
                ProTeamName = this.ProIdTeamNavigation.TemName
            };
        }
    }
}
