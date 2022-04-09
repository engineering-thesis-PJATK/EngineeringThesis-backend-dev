using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeams();
        Task<Team> GetTeamById(int teamId);
    }
}