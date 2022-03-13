using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int CurId { get; set; }
        public string CurComments { get; set; }
        public string CurPosition { get; set; }
        public int CurIdCompany { get; set; }
        public int CurIdPerson { get; set; }

        public virtual Company CurIdCompanyNavigation { get; set; }
        public virtual Person CurIdPersonNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
