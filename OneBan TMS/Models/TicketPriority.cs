using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TicketPriority
    {
        public TicketPriority()
        {
            Tickets = new HashSet<Ticket>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TpiId { get; set; }
        public int TpiWeight { get; set; }
        public string TpiDescription { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
