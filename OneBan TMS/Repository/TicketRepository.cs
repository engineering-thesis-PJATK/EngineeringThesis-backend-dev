using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly OneManDbContext _context;
        public TicketRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetTickets()
        {
            return await _context
                .Tickets
                .ToListAsync();
        }
        public async Task<Ticket> GetTicketById(int ticketId)
        {
            Ticket ticket = await _context
                .Tickets
                .Where(x => x.TicId == ticketId)
                .SingleOrDefaultAsync();
            return ticket;
        }
    }
}