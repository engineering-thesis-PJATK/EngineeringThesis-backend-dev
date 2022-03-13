using System.Collections.Generic;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface ITicket
    {
        Ticket GetTicketById(int ticketId);
        IList<Ticket> GetAllTickets();
    }
}