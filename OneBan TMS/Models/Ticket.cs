using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Correspondences = new HashSet<Correspondence>();
            EmployeeTickets = new HashSet<EmployeeTicket>();
            TicketNotes = new HashSet<TicketNote>();
            TimeEntries = new HashSet<TimeEntry>();
        }

        public int TicId { get; set; }
        public string TicName { get; set; }
        public string TicTopic { get; set; }
        public string TicDescription { get; set; }
        public decimal TicEstimatedCost { get; set; }
        public DateTime TicCreatedAt { get; set; }
        public DateTime TicDueDate { get; set; }
        public DateTime? TicCompletedAt { get; set; }
        public int TicIdStatus { get; set; }
        public int TicIdClient { get; set; }
        public int TicIdTicketType { get; set; }
        public int TicIdTicketPriority { get; set; }

        public virtual Customer TicIdClientNavigation { get; set; }
        public virtual Status TicIdStatusNavigation { get; set; }
        public virtual TicketPriority TicIdTicketPriorityNavigation { get; set; }
        public virtual TicketType TicIdTicketTypeNavigation { get; set; }
        public virtual ICollection<Correspondence> Correspondences { get; set; }
        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual ICollection<TicketNote> TicketNotes { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
