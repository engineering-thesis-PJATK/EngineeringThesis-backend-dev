
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly OneManDbContext _context;
        public TeamRepository(OneManDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context
                .Teams
                .ToListAsync();
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            return await _context
                .Teams
                .Where(x => x.TemId == teamId)
                .SingleOrDefaultAsync();
        }

        public async Task AddNewTeam(TeamDto teamDto)
        {
            if (teamDto.Name != "")
            {
                Team newTeam = new Team()
                {
                    TemName = teamDto.Name
                };
                _context.Teams.Add(newTeam);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTeam(TeamDto teamDto, int teamId)
        {
            Team teamToUpdate = await _context
                .Teams
                .Where(x => x.TemId == teamId)
                .SingleOrDefaultAsync();
            if (!(teamToUpdate is null))
            {
                teamToUpdate.TemName = teamDto.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTeam(int teamId)
        {
            Team teamToDelete = await _context
                .Teams
                .Where(x => x.TemId == teamId)
                .SingleOrDefaultAsync();
            if (!(teamToDelete is null))
            {
                _context.Teams.Remove(teamToDelete);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}