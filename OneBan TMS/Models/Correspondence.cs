using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Correspondence
    {
        public Correspondence()
        {
            CorrespondenceAttachments = new HashSet<CorrespondenceAttachment>();
        }

        public int CorId { get; set; }
        public string CorSender { get; set; }
        public string CorReceiver { get; set; }
        public string CorSubject { get; set; }
        public string CorBody { get; set; }
        public DateTime? CorSentAt { get; set; }
        public DateTime? CorReceivedAt { get; set; }
        public int CorIdTicket { get; set; }

        public virtual Ticket CorIdTicketNavigation { get; set; }
        public virtual ICollection<CorrespondenceAttachment> CorrespondenceAttachments { get; set; }
    }
}
