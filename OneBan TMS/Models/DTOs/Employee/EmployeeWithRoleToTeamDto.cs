using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs.Employee
{
    public class EmployeeWithRoleToTeamDto
    {
        public int TeamId { get; set; }
        public IEnumerable<EmployeeWithRole> EmployeesWithRoles { get; set; } 
    }
}