using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketNote
    {
        public int TntId { get; set; }
        public int TntIdTicket { get; set; }
        public string TntContent { get; set; }

        public virtual Ticket TntIdTicketNavigation { get; set; }
    }
}
