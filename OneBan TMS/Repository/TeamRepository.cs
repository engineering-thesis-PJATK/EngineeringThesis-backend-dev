using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Repository
{
    public class TeamRepository :ITeamRepository
    {
        private readonly OneManDbContext _context;

        public TeamRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<List<Team>> GetTeams()
        {
            var teams = await _context
                                        .Teams
                                        .ToListAsync();
            if (!(teams.Any()))
            {
                return 
                    null;
            }
            return
                teams;
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            Team team = await _context
                              .Teams
                              .Where(team => team.TemId == teamId)
                              .SingleOrDefaultAsync();
            return
                team;

        }
    }
}