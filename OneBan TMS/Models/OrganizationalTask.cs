using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OrganizationalTask
    {
        public int OtkId { get; set; }
        public string OtkDescription { get; set; }
        public int OtkIdEmployee { get; set; }
        public int OtkIdOrganizationalTaskStatus { get; set; }

        public virtual Employee OtkIdEmployeeNavigation { get; set; }
        public virtual OrganizationalTaskStatus OtkIdOrganizationalTaskStatusNavigation { get; set; }
    }
}
