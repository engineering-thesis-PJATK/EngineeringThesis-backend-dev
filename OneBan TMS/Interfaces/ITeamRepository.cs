using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeamById(int teamId);
        Task AddNewTeam(TeamDto teamDto);
        Task UpdateTeam(TeamDto teamDto, int teamId);
        Task DeleteTeam(int teamId);
    }
}