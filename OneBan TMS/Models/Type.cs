using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Type
    {
        public Type()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TypId { get; set; }
        public string TypName { get; set; }
        public string TypDescription { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
