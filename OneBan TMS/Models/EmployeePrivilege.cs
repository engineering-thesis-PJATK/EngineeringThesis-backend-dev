using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public string EpvDescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
    }
}
