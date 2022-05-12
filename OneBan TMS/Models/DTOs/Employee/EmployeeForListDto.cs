using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs.Employee
{
    using Models;
    public class EmployeeForListDto
    {
        public int EmpId { get; set; }
        public string EmpLogin { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        public IEnumerable<EmployeePrivilege> Roles { get; set; }
        public IEnumerable<Team> EmployeeTeams { get; set; }
    }
}