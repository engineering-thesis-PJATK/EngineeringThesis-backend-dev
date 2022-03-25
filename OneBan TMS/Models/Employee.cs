using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeePrivilegeEmployees = new HashSet<EmployeePrivilegeEmployee>();
            EmployeeTeams = new HashSet<EmployeeTeam>();
            EmployeeTickets = new HashSet<EmployeeTicket>();
            OrganizationalTasks = new HashSet<OrganizationalTask>();
        }

        public int EmpId { get; set; }
        public string EmpLogin { get; set; }
        [JsonIgnore]
        public string EmpPassword { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        [JsonIgnore]
        public DateTime EmpCreatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrganizationalTask> OrganizationalTasks { get; set; }
    }
}
