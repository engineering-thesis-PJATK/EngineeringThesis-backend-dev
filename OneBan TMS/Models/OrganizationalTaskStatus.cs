using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OrganizationalTaskStatus
    {
        public OrganizationalTaskStatus()
        {
            OrganizationalTasks = new HashSet<OrganizationalTask>();
        }

        public int OtsId { get; set; }
        public string OtsName { get; set; }
        public string OtsDescription { get; set; }

        public virtual ICollection<OrganizationalTask> OrganizationalTasks { get; set; }
    }
}
