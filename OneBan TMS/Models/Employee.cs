using System;
using System.Collections.Generic;

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
            TimeEntries = new HashSet<TimeEntry>();
        }

        public int EmpId { get; set; }
        public string EmpLogin { get; set; }
        public string EmpPassword { get; set; }
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        public DateTime EmpCreatedAt { get; set; }

        public virtual ICollection<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual ICollection<OrganizationalTask> OrganizationalTasks { get; set; }
        public virtual ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
