using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OrganizationalTask
    {
        public int OtkId { get; set; }
        public int OtkIdEmployee { get; set; }
        public string OtkDescription { get; set; }

        public virtual Employee OtkIdEmployeeNavigation { get; set; }
    }
}
