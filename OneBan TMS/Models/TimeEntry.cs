using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TimeEntry
    {
        public int TesId { get; set; }
        public int? TesProjectTask { get; set; }
        public int? TesIdTicket { get; set; }
        public DateTime? TesStartingDate { get; set; }
        public DateTime? TesCompletionDate { get; set; }
        public DateTime? TesEntryDate { get; set; }
        public TimeSpan? TesEntryTime { get; set; }

        public virtual Ticket TesIdTicketNavigation { get; set; }
        public virtual ProjectTask TesProjectTaskNavigation { get; set; }
    }
}
