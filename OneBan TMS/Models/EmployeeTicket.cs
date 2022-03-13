using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeeTicket
    {
        public int EtsId { get; set; }
        public int EtsEmployeeId { get; set; }
        public int EtsIdTicket { get; set; }

        public virtual Employee EtsEmployee { get; set; }
        public virtual Ticket EtsIdTicketNavigation { get; set; }
    }
}
