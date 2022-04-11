using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeeDto();
        Task<EmployeeDto> GetEmployeeByIdDto(int employeeId);
        Task UpdateEmployee(int employeeId, EmployeeToUpdate employeeToUpdate);
        Task<bool> ExistsEmployee(int employeeId);
        Task<List<EmployeePrivilegeGetDto>> GetEmployeePrivileges();
        Task<EmployeePrivilegeGetDto> GetEmployeePrivilegeById(int privilegeId);

    }
}