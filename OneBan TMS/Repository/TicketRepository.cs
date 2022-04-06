using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

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
        
        public async Task<List<TicketTypeDto>> GetTicketTypes()
        {
            var ticketTypes = await _context
                                                  .TicketTypes
                                                  .ToListAsync();
            return ticketTypes.Select(ticketType => new TicketTypeDto()
            {
                Description = ticketType.TtpDescription, 
                Id = ticketType.TtpId, 
                Name = ticketType.TtpName
            }).ToList();
        }

        public async Task<List<TicketPriorityDto>> GetTicketPriorities()
        {
            var ticketPriorities = await _context
                                                         .TicketPriorities
                                                         .ToListAsync();
            return ticketPriorities.Select(ticketPriority => new TicketPriorityDto()
            {
                Description = ticketPriority.TpiDescription, 
                Id = ticketPriority.TpiId,
                Weight = ticketPriority.TpiWeight
            }).ToList();
        }
    }
}