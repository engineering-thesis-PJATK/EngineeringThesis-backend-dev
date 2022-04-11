using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneBan_TMS.Models.DTOs.Employee
{
    public class EmployeeDto
    {
        public string EmpName { get; set; }
        public string EmpSurname { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhoneNumber { get; set; }
        public string EmpPassword { get; set; }
    }

}
