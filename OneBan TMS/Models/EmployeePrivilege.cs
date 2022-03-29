using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeePrivilege
    {
        public EmployeePrivilege()
        {
            EmployeePrivilegeEmployees = new HashSet<EmployeePrivilegeEmployee>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpvId { get; set; }
        public string EpvName { get; set; }
        public string EpvDescription { get; set; }

        public virtual ICollection<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
    }
}
