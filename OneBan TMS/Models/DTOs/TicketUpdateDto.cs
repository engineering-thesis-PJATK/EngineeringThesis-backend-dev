using System;
using System.ComponentModel.DataAnnotations;

namespace OneBan_TMS.Models.DTOs
{
    public class TicketUpdateDto
    {
        [Required]
        public string TicName { get; set; }
        
        [Required]
        public string TicTopic { get; set; }
        
        public decimal TicEstimatedCost { get; set; }
        
        [Required]
        public DateTime TicDueDate { get; set; }
        
        public DateTime TicCompletedAt { get; set; }
        
        public string TicDescription { get; set; }
        
        [Required]
        public int TicTicketStatusId { get; set; }
        
        [Required]
        public int TicCustomerId { get; set; }
        
        [Required]
        public int TicTicketTypeId { get; set; }
        
        [Required]
        public int TicTicketPriorityId { get; set; }
    }
}