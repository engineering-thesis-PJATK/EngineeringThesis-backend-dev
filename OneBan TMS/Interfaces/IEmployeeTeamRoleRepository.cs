using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IEmployeeTeamRoleRepository
    {
        Task<IEnumerable<EmployeeTeamRole>> GetEmployeeTeamRoles();
        Task<EmployeeTeamRole> GetEmployeeTeamRoleById(int employeeTeamRoleId);
        Task AddNewEmployeeTeamRole(EmployeeTeamRoleDto employeeTeamRoleDto);
        Task UpdateEmployeeTeamRole(EmployeeTeamRoleDto employeeTeamRoleDto, int employeeTeamRoleId);
        Task DeleteEmployeeTeamRole(int employeeTeamRoleId);
    }
}