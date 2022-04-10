using System.ComponentModel.DataAnnotations;

namespace OneBan_TMS.Models.DTOs
{
    public class TicketStatusIdPatchDto
    {
        [Required]
        public int TstId { get; set; }
    }
}