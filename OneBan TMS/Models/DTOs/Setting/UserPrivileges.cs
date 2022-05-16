using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs.Setting
{
    public class UserPrivileges
    {
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public List<PrivilegeSettings> EmpPrivileges { get; set; }
    }
}