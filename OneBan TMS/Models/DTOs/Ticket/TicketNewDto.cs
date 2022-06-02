using System;
using System.ComponentModel.DataAnnotations;

namespace OneBan_TMS.Models.DTOs.Ticket
{
    public class TicketNewDto
    {
        public string TicTopic { get; set; }
        public decimal TicEstimatedCost { get; set; }
        public DateTime TicDueDate { get; set; }
        public DateTime? TicCompletedAt { get; set; }
        public string TicDescription { get; set; }
        public int TicIdTicketStatus { get; set; }
        public int TicIdCustomer { get; set; }
        public int TicIdTicketType { get; set; }
        public int TicIdTicketPriority { get; set; }
    }
}