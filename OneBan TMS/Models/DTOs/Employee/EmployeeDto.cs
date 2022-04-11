using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs
{
    public class EmployeeDto : Employee
    {
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<Team> EmployeeTeams { get; set; }
    }
}