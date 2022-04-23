using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Team;

namespace OneBan_TMS.Repository
{
    public class TeamRepository :ITeamRepository
    {
        private readonly OneManDbContext _context;

        public TeamRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<List<TeamGetDto>> GetTeams()
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
                teams
                .Select(ChangeTeamBaseToGetDto)
                .ToList();
        }

        public async Task<TeamGetDto> GetTeamById(int teamId)
        {
            Team team = await _context
                              .Teams
                              .Where(team => team.TemId == teamId)
                              .SingleOrDefaultAsync();
            return
                ChangeTeamBaseToGetDto(team);

        }

        public async Task<TeamGetDto> PostTeam(TeamUpdateDto newTeam)
        {
            Team team = await _context
                              .Teams
                              .Where(team => team.TemName.Equals(newTeam.TemName))
                              .SingleOrDefaultAsync();
            if (team is not null)
            {
                return
                    null;
            }

            Team teamToAdd = new()
            {
                TemName = newTeam.TemName
            };
            await _context
                .Teams
                .AddAsync(teamToAdd);
            await _context
                  .SaveChangesAsync();

            return
                GetTeamById(teamToAdd.TemId)
                .Result;
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

        public async Task<TeamGetDto> UpdateTeamById(int teamId, TeamUpdateDto teamUpdateDto)
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
        private TeamGetDto ChangeTeamBaseToGetDto(Team team)
        {
            return 
                new TeamGetDto()
                {   
                  TemName = team.TemName,
                  TemId = team.TemId
                };
        }
    }
}