using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketNote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TntId { get; set; }
        public string TntContent { get; set; }
        public int TntIdTicket { get; set; }

        public virtual Ticket TntIdTicketNavigation { get; set; }
    }
}
