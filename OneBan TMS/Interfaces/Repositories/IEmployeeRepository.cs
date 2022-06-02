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
        Task<Employee> AddEmployee(EmployeeDto employee);
        Task UpdateEmployee(int employeeId, EmployeeToUpdate employeeToUpdate);
        Task<bool> ExistsEmployee(int employeeId);
        Task<bool> ExistsEmployeeByEmail(string employeeEmail);
        Task<List<EmployeePrivilegeGetDto>> GetEmployeePrivileges();
        Task<EmployeePrivilegeGetDto> GetEmployeePrivilegeById(int privilegeId);
        Task<bool> ExistsEmployeePrivileges(List<int> privileges);
        Task<string> ChangePassword(string email);
        Task ManagePrivilegesToUser(int employeeId, List<int> privileges);
        Task DeleteEmployee(int employeeId);
    }
}