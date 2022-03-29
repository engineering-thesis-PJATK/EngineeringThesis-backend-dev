using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class TimeEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TesId { get; set; }
        public DateTime TesCreatedAt { get; set; }
        public DateTime TesUpdatedAt { get; set; }
        public DateTime TesEntryDate { get; set; }
        public TimeSpan TesEntryTime { get; set; }
        public string TesDescription { get; set; }
        public int? TesIdProjectTask { get; set; }
        public int? TesIdTicket { get; set; }

        public virtual ProjectTask TesIdProjectTaskNavigation { get; set; }
        public virtual Ticket TesIdTicketNavigation { get; set; }
    }
}
