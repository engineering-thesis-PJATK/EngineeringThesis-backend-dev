using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Setting;

namespace OneBan_TMS.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly OneManDbContext _context;
        public SettingRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserPrivileges>> GetUserWithPrivileges()
        {
            List<UserPrivileges> userWithPrivilegesList = new List<UserPrivileges>();
            var data = await _context
                .Employees
                .Include(x => x.EmployeePrivilegeEmployees)
                .ThenInclude(x => x.EpeIdEmployeePrivilageNavigation)
                .Select(x => new
                {
                    x.EmpId,
                    x.EmpEmail,
                    x.EmpName,
                    x.EmpSurname,
                    x.EmployeePrivilegeEmployees
                })
                .ToListAsync();
            foreach (var userData in data)
            {
                List<PrivilegeSettings> privilegesForUser = new List<PrivilegeSettings>();
                foreach (var privilege in userData.EmployeePrivilegeEmployees)
                {
                    var userPrivilegesData = await _context
                        .EmployeePrivileges
                        .Where(x => x.EpvId == privilege.EpeIdEmployeePrivilage)
                        .FirstOrDefaultAsync();
                    privilegesForUser.Add(new PrivilegeSettings()
                    {
                        EpvId = userPrivilegesData.EpvId,
                        EpvName = userPrivilegesData.EpvName
                    });
                }
                userWithPrivilegesList.Add(new UserPrivileges()
                {
                    EmpId = userData.EmpId,
                    EmpEmail = userData.EmpEmail,
                    EmpName = userData.EmpName,
                    EmpSurname = userData.EmpSurname,
                    EmpPrivileges = privilegesForUser
                });
            }

            return userWithPrivilegesList;
        }

        public async Task<TicketPriority> AddTicketPriority(NewTicketPriorityDto newTicketPriorityDto)
        {
            TicketPriority ticketPriority = new TicketPriority()
            {
                TpiDescription = newTicketPriorityDto.TpiDescription,
                TpiWeight = newTicketPriorityDto.TpiWeight
            };
            await _context.TicketPriorities.AddAsync(ticketPriority);
            await _context.SaveChangesAsync();
            return ticketPriority;
        }

        public async Task<TicketType> AddTicketType(NewTicketTypeDto newTicketTypeDto)
        {
            TicketType ticketType = new TicketType()
            {
                TtpDescription = newTicketTypeDto.TtpDescription,
                TtpName = newTicketTypeDto.TtpName
            };
            await _context.TicketTypes.AddAsync(ticketType);
            await _context.SaveChangesAsync();
            return ticketType;
        }

        public async Task<TicketStatus> AddTicketStatus(NewTicketStatusDto newTicketStatusDto)
        {
            TicketStatus ticketStatus = new TicketStatus()
            {
                TstDescription = newTicketStatusDto.TstDescription,
                TstName = newTicketStatusDto.TstName
            };
            await _context.TicketStatuses.AddAsync(ticketStatus);
            await _context.SaveChangesAsync();
            return ticketStatus;
        }

        public async Task<OrganizationalTaskStatus> AddOrganizationalTaskStatus(NewOrganizationalTaskStatusDto newOrganizationalTaskStatus)
        {
            OrganizationalTaskStatus organizationalTaskStatus = new OrganizationalTaskStatus()
            {
                OtsName = newOrganizationalTaskStatus.OtsName,
                OtsDescription = newOrganizationalTaskStatus.OtsDescription
            };
            await _context.OrganizationalTaskStatuses.AddAsync(organizationalTaskStatus);
            await _context.SaveChangesAsync();
            return organizationalTaskStatus;
        }
    }
}