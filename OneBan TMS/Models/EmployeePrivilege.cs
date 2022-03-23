using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeePrivilege
    {
        public EmployeePrivilege()
        {
            EmployeePrivilegeEmployees = new HashSet<EmployeePrivilegeEmployee>();
        }

        public int EpvId { get; set; }
        public string EpvName { get; set; }
        public string EpvDescription { get; set; }

        public virtual ICollection<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
    }
}
