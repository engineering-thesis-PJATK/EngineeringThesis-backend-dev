using System;

namespace OneBan_TMS.Models.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public int IdCompany { get; set; }
        public int IdTeam { get; set; }
    }
}