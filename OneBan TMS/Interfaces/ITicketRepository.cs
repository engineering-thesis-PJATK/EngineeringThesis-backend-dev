using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ITicketRepository
    {
        Task<TicketDto> GetTicketById(int ticketId);
        Task<List<TicketDto>> GetTickets();
        Task<List<TicketTypeDto>> GetTicketTypes();
        Task<List<TicketPriorityDto>> GetTicketPriorities();
        Task<TicketDto> UpdateTicket(int ticketId, TicketUpdateDto ticketUpdate);
        Task<TicketDto> UpdateTicketStatusId(int ticketId, int ticketStatusId);
    }
}