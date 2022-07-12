using System;

namespace OneBan_TMS.Models.DTOs.Project
{
    using OneBan_TMS.Models;

    public class ProjectNewDto
    {
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public DateTime ProCreatedAt { get; set; }
        public DateTime? ProCompletedAt { get; set; }
        public int ProIdCompany { get; set; }
        public int ProIdTeam { get; set; }
        public int ProStatusId { get; set; }

        public Project GetProject()
        {
            return new Project()
            {
                ProName = this.ProName,
                ProDescription = this.ProDescription,
                ProCreatedAt = this.ProCreatedAt,
                ProCompletedAt = this.ProCompletedAt,
                ProIdCompany = this.ProIdCompany,
                ProIdTeam = this.ProIdTeam,
                ProIdProjectStatus = ProStatusId
            };
        }
    }
}