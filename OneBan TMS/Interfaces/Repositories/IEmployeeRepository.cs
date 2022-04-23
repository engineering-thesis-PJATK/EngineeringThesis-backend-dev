using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeForListDto>> GetAllEmployeeDto();
        Task<EmployeeForListDto> GetEmployeeByIdDto(int employeeId);
        Task AddEmployee(EmployeeDto employee);
        Task UpdateEmployee(int employeeId, EmployeeToUpdate employeeToUpdate);
        Task<bool> ExistsEmployee(int employeeId);
        Task<bool> ExistsEmployeeByEmail(string employeeEmail);
        Task<List<EmployeePrivilegeGetDto>> GetEmployeePrivileges();
        Task<EmployeePrivilegeGetDto> GetEmployeePrivilegeById(int privilegeId);
        Task<bool> ExistsEmployeePrivileges(List<int> privileges);
    }
}