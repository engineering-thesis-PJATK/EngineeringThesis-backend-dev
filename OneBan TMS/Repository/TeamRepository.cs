using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Models.DTOs.Team;

namespace OneBan_TMS.Repository
{
    public class TeamRepository :ITeamRepository
    {
        private readonly OneManDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeTeamRoleRepository _employeeTeamRoleRepository;
        public TeamRepository(OneManDbContext context, IEmployeeRepository employeeRepository, IEmployeeTeamRoleRepository employeeTeamRoleRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _employeeTeamRoleRepository = employeeTeamRoleRepository;
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

        public async Task AddEmployeesToTeamWithRoles(EmployeeWithRoleToTeamDto employeeWithRoleToTeamDto)
        {
            if (!(await ExistsTeam(employeeWithRoleToTeamDto.TeamId)))
                throw new ArgumentException("Team does not exists");
            foreach (var employeeWithRole in employeeWithRoleToTeamDto.EmployeesWithRoles)
            {
                if (!(await _employeeRepository.ExistsEmployee(employeeWithRole.employeeId)))
                    throw new ArgumentException("Employee does not exists");
                if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(employeeWithRole.teamRoleId)))
                    throw new ArgumentException("Employee team role does not exists");
            }
            foreach (var employeesWithRoles in employeeWithRoleToTeamDto.EmployeesWithRoles)
            {
                await _context.AddAsync(new EmployeeTeam()
                {
                    EtmIdTeam = employeeWithRoleToTeamDto.TeamId,
                    EtmIdRole = employeesWithRoles.teamRoleId,
                    EtmIdEmployee = employeesWithRoles.employeeId
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsTeam(int teamId)
        {
            var result = await _context.Teams
                .AnyAsync(x => x.TemId == teamId);
            return result;
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