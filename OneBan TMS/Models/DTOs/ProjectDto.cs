using System;

namespace OneBan_TMS.Models.DTOs
{
    public class ProjectDto
    {
        public int ProId { get; set; }
        public string ProName { get; set; }
        public string ProDescription { get; set; }
        public DateTime ProCreatedAt { get; set; }
        public DateTime ProCompletedAt { get; set; }
        public int ProIdCompany { get; set; }
        public int ProIdTeam { get; set; }
    }
}