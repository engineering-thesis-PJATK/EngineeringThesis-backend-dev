using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Kanban;
using OneBan_TMS.Models.DTOs.Ticket;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<TicketDto> GetTicketById(int ticketId);
        Task<List<TicketDto>> GetTickets();
        Task<List<TicketTypeDto>> GetTicketTypes();
        Task<TicketTypeDto> GetTicketTypeById(int ticketTypeId);
        Task<List<TicketPriorityDto>> GetTicketPriorities();
        Task<TicketPriorityDto> GetTicketPriorityById(int ticketPriorityId);
        Task<TicketDto> UpdateTicket(int ticketId, TicketUpdateDto ticketUpdate);
        Task<TicketDto> UpdateTicketStatusId(int ticketId, int ticketStatusId);
        Task<List<TicketStatusDto>> GetTicketStatuses();
        Task<TicketStatusDto> GetTicketStatusById(int ticketStatusId);
        Task DeleteTicketById(int ticketId);
        Task<List<KanbanElement>> GetTicketsForEmployeeByStatus(int statusId, int employeeId);
        Task UpdateTicketStatus(int ticketId, int statusId);
<<<<<<< HEAD
        Task<int> GetTicketStatusId(string status);
        Task<Ticket> AddTicket(TicketNewDto newTicket);
=======
        Task<List<TicketCustomerCompanyDto>>GetTicketsForCustomTicketList();
>>>>>>> PŁ
    }
}