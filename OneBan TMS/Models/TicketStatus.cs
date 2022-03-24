using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketStatus
    {
        public TicketStatus()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TstId { get; set; }
        public string TstName { get; set; }
        public string TstDescription { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
