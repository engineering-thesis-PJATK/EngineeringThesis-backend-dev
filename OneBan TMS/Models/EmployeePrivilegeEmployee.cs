using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeePrivilegeEmployee
    {
        public int EpeId { get; set; }
        public int EpeIdEmployee { get; set; }
        public int EpeIdEmployeePrivilage { get; set; }

        public virtual Employee EpeIdEmployeeNavigation { get; set; }
        public virtual EmployeePrivilege EpeIdEmployeePrivilageNavigation { get; set; }
    }
}
