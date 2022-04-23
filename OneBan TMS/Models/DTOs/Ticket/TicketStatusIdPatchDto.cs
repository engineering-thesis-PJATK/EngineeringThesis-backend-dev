using System.ComponentModel.DataAnnotations;

namespace OneBan_TMS.Models.DTOs.Ticket
{
    public class TicketStatusIdPatchDto
    {
        [Required]
        public int TstId { get; set; }
    }
}