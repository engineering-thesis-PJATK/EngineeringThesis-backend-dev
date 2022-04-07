using System;
using System.ComponentModel.DataAnnotations;

namespace OneBan_TMS.Models.DTOs
{
    public class TicketUpdateDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Topic { get; set; }
        
        public decimal EstimatedCost { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        public DateTime CompletedAt { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public int TicketStatusId { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public int TicketTypeId { get; set; }
        
        [Required]
        public int TicketPriorityId { get; set; }
    }
}