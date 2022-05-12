using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class StatusHandler : IStatusHandler
    {
        private readonly OneManDbContext _context;
        public StatusHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsStatus(int statusId)
        {
            var result = await _context
                .TicketStatuses
                .Where(x => x.TstId == statusId)
                .AnyAsync();
            return result;
        }
    }
}