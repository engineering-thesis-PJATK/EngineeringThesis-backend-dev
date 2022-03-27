using System.Collections.Generic;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDto> GetAllEmployeeDto();
        EmployeeDto GetEmployeeByIdDto(int idEmployee);
    }
}