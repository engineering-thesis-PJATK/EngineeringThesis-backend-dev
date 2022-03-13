using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class Person
    {
        public Person()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
        }

        public int PerId { get; set; }
        public string PerName { get; set; }
        public string PerSurname { get; set; }
        public string PerEmail { get; set; }
        public string PerPhoneNumber { get; set; }
        public DateTime PerAccountCreationTimestamp { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
