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
        public string TicTittle { get; set; }
        public int TicIdStatus { get; set; }
        public string TicName { get; set; }
        public string TicDescription { get; set; }
        public decimal TicEstCost { get; set; }
        public int TicPriority { get; set; }
        public int TicIdClient { get; set; }
        public int TicIdType { get; set; }
        public DateTime TicCreationTime { get; set; }
        public DateTime TicDueTime { get; set; }

        public virtual Customer TicIdClientNavigation { get; set; }
        public virtual Status TicIdStatusNavigation { get; set; }
        public virtual Type TicIdTypeNavigation { get; set; }
        public virtual ICollection<Correspondence> Correspondences { get; set; }
        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual ICollection<TicketNote> TicketNotes { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
