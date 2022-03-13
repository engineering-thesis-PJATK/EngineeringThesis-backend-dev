using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Repository
{
    public class TicketRepository : ITicket
    {
        private OneManDbContext dbContext;
        public TicketRepository(OneManDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IList<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = dbContext.Tickets.ToList();
            return tickets;
        }

        public Ticket GetTicketById(int ticketId)
        {
            Ticket ticket = dbContext.Tickets.Where(ticket => ticket.TicId == ticketId).Select(ticket => ticket)
                .SingleOrDefault();
            return ticket;
        }
    }
}