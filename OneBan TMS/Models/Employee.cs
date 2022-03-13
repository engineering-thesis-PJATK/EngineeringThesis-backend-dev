using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeTeams = new HashSet<EmployeeTeam>();
            EmployeeTickets = new HashSet<EmployeeTicket>();
            OrganizationalTasks = new HashSet<OrganizationalTask>();
        }

        public int EmpId { get; set; }
        public int EmpPrivelages { get; set; }
        public string EmpLogin { get; set; }
        public string EmpPassword { get; set; }
        public int EmpIdPerson { get; set; }
        public bool EmpIsAdmin { get; set; }

        public virtual Person EmpIdPersonNavigation { get; set; }
        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
        public virtual ICollection<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual ICollection<OrganizationalTask> OrganizationalTasks { get; set; }
    }
}
