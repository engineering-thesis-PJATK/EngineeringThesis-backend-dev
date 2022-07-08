using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.TeamMember;

namespace OneBan_TMS.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly OneManDbContext _context;

        public TeamMemberRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task AddTeamMember(TeamMemberAddDto newTeamMemberAddDto)
        {
            bool isMemberExists = await _context.EmployeeTeams
                .AnyAsync(x => 
                                  x.EtmIdTeam == newTeamMemberAddDto.TmrIdTeam
                               && x.EtmIdEmployee == newTeamMemberAddDto.TmrIdEmployee
                               && x.EtmIdRole == newTeamMemberAddDto.TmrIdRole);
            if (isMemberExists)
                throw new ArgumentException("Member exist in database");
            var newEmployeeTeam = newTeamMemberAddDto.GetEmployeeTeam();
            await _context.EmployeeTeams.AddAsync(newEmployeeTeam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeamMember(int teamId, int employeeId)
        {
            EmployeeTeam employeeTeamToDelete = await _context.EmployeeTeams
                .SingleOrDefaultAsync(x => 
                                              x.EtmIdEmployee == employeeId
                                           && x.EtmIdTeam == teamId);
            _context.EmployeeTeams.Remove(employeeTeamToDelete);
            await _context.SaveChangesAsync();
        }

        public Task UpdateTeamMemberRole(int teamId, int employeeId, int newRoleId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsTeamsMember(int teamId, int employeeId, int roleId)
        {
            throw new System.NotImplementedException();
        }
    }
}