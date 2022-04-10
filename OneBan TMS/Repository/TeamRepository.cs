using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

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

        public async Task DeleteTeamById(int teamId)
        {
            Team team = await _context
                              .Teams
                              .Where(team => team.TemId == teamId)
                              .SingleOrDefaultAsync();
            _context
            .Teams
            .Remove(team);
            await _context
                .SaveChangesAsync();
            
        }

        public async Task<Team> UpdateTeamById(int teamId, TeamUpdateDto teamUpdateDto)
        {
            Team team = await _context
                              .Teams
                              .Where(team => team.TemId == teamId)
                              .SingleOrDefaultAsync();
            if (team is not null)
            {
                team.TemName = teamUpdateDto.TemName;
                await _context
                      .SaveChangesAsync();
                return
                    GetTeamById(teamId)
                    .Result;
            }

            return
                null;
        }
    }
}