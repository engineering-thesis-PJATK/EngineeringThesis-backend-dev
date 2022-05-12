using System;

namespace OneBan_TMS.Models.DTOs.Ticket
{
    public class TicketDto
    {
        public int TicId { get; set; }
        public string TicName { get; set; }
        public string TicTopic { get; set; }
        public decimal TicEstimatedCost { get; set; }
        public DateTime TicDueDate { get; set; }
        public DateTime TicCompletedAt { get; set; }
        public DateTime TicCreatedAt { get; set; }
        public string TicDescription { get; set; }
        public int TicTicketStatusId { get; set; }
        public int TicCustomerId { get; set; }
        public int TicTicketTypeId { get; set; }
        public int TicTicketPriorityId { get; set; }
    }
}