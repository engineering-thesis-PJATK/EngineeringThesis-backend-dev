using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
<<<<<<< .merge_file_G0H5zq
=======
using OneBan_TMS.Models.DTOs.Employee;
>>>>>>> .merge_file_z1sCI0
using OneBan_TMS.Models.DTOs.Team;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        Task<List<TeamGetDto>> GetTeams();
        Task<TeamGetDto> GetTeamById(int teamId);
        Task<TeamGetDto> PostTeam(TeamUpdateDto newTeam);
        Task<TeamGetDto> UpdateTeamById(int teamId,TeamUpdateDto teamUpdateDto);
        Task DeleteTeamById(int teamId);
<<<<<<< .merge_file_G0H5zq
=======
        Task AddEmployeesToTeamWithRoles(EmployeeWithRoleToTeamDto employeeWithRoleToTeamDto);
        Task<bool> ExistsTeam(int teamId);
>>>>>>> .merge_file_z1sCI0
    }
}