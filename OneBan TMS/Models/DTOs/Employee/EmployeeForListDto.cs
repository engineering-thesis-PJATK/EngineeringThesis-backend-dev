using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs
{
    public class EmployeeForListDto
    {
        public int EmpId { get; set; }
        public string EmpLogin { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<Team> EmployeeTeams { get; set; }
    }
}