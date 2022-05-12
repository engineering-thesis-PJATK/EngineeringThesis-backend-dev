using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketPriority
    {
        public TicketPriority()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TpiId { get; set; }
        public int TpiWeight { get; set; }
        public string TpiDescription { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
