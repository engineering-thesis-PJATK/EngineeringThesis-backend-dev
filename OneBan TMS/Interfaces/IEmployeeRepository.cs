using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeeDto();
        Task<EmployeeDto> GetEmployeeByIdDto(int idEmployee);
        Task<List<EmployeePrivilegeGetDto>> GetEmployeePrivileges();
        Task<EmployeePrivilegeGetDto> GetEmployeePrivilegeById(int privilegeId);

    }
}