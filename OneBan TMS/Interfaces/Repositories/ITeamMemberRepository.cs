using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.TeamMember;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ITeamMemberRepository
    {
        Task AddTeamMember(TeamMemberAddDto newTeamMemberAddDto);
        Task DeleteTeamMember(int teamId, int employeeId);
        Task UpdateTeamMemberRole(int teamId, int employeeId, int newRoleId);
        Task<bool> ExistsTeamsMember(int teamId, int employeeId, int roleId);
        
    }
}