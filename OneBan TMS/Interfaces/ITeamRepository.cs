using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeams();
        Task<Team> GetTeamById(int teamId);
        Task DeleteTeamById(int teamId);
        Task<Team> UpdateTeamById(int teamId,TeamUpdateDto teamUpdateDto);
    }
}