using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Setting;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        Task<IEnumerable<UserPrivileges>> GetUserWithPrivileges();
        Task<TicketPriority> AddTicketPriority(NewTicketPriorityDto ticketPriorityDto);
        Task<TicketType> AddTicketType(NewTicketTypeDto ticketTypeDto);
        Task<TicketStatus> AddTicketStatus(NewTicketStatusDto ticketStatusDto);
        Task<OrganizationalTaskStatus> AddOrganizationalTaskStatus(NewOrganizationalTaskStatusDto newOrganizationalTaskStatus);
    }
}