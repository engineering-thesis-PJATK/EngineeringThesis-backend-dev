using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<TeamGetDto>> GetTeams();
        Task<TeamGetDto> GetTeamById(int teamId);
        Task<TeamGetDto> PostTeam(TeamUpdateDto newTeam);
        Task<TeamGetDto> UpdateTeamById(int teamId,TeamUpdateDto teamUpdateDto);
        Task DeleteTeamById(int teamId);
    }
}