using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class EmployeePrivilegeEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpeId { get; set; }
        public int EpeIdEmployee { get; set; }
        public int EpeIdEmployeePrivilage { get; set; }
        [JsonIgnore]
        public virtual Employee EpeIdEmployeeNavigation { get; set; }
        [JsonIgnore]
        public virtual EmployeePrivilege EpeIdEmployeePrivilageNavigation { get; set; }
    }
}
