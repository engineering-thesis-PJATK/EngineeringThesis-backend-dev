using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TtpId { get; set; }
        public string TtpName { get; set; }
        public string TtpDescription { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
