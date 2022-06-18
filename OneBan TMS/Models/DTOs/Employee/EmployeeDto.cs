using System;
using OneBan_TMS.Abstracts.Employee;

namespace OneBan_TMS.Models.DTOs.Employee
{
    using OneBan_TMS.Models;
    public class EmployeeDto : EmployeeAbstract
    {
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        public string EmpPassword { get; set; }
        public override Employee GetEmployee()
        {
            return new Employee()
            {
                EmpEmail = this.EmpEmail,
                EmpSurname = this.EmpSurname,
                EmpName = this.EmpName,
                EmpPhoneNumber = EmpPhoneNumber,
                EmpCreatedAt = DateTime.Now
            };
        }
    }

}
