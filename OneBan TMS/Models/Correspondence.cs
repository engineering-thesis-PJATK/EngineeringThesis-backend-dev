using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Correspondence
    {
        public Correspondence()
        {
            Attachments = new HashSet<Attachment>();
        }

        public int CorId { get; set; }
        public int CorSubject { get; set; }
        public int CorBody { get; set; }
        public DateTime? CorReceived { get; set; }
        public DateTime? CorSent { get; set; }
        public string CorSender { get; set; }
        public string CorReceiver { get; set; }
        public int CorIdTicket { get; set; }

        public virtual Ticket CorIdTicketNavigation { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
