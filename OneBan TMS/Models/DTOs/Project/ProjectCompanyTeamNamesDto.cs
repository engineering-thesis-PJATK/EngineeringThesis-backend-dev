using System;

namespace OneBan_TMS.Models.DTOs.Project
{
    using OneBan_TMS.Models;
    public class ProjectCompanyTeamNamesDto 
    {
        public int ProId { get; set; }
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public DateTime ProCreatedAt { get; set; }
        public DateTime? ProCompletedAt { get; set; }
        public int ProIdCompany { get; set; }
        public int ProIdTeam { get; set; }
        public int ProIdProjectStatus { get; set; }
        public string ProCompanyName { get; set; }
        public string ProTeamName { get; set; }
    }
}