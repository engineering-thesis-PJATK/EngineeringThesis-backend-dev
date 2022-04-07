using System;
using Microsoft.Graph;

namespace OneBan_TMS.Models.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public decimal EstimatedCost { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public int TicketStatusId { get; set; }
        public int CustomerId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
    }
}