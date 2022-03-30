using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketById(int ticketId);
        Task<List<Ticket>> GetTickets();
    }
}