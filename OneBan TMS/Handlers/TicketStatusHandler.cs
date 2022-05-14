using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class TicketStatusHandler : ITicketStatusHandler
    {
        private readonly OneManDbContext _context;
        public TicketStatusHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TicketStatusExists(string statusName)
        {
            return await _context
                .TicketStatuses
                .AnyAsync(x => x.TstName.Equals(statusName));
        }
    }
}