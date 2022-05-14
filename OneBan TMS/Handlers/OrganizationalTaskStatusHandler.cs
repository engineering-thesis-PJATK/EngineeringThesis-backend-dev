using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class OrganizationalTaskStatusHandler : IOrganizationalTaskStatusHandler
    {
        private readonly OneManDbContext _context;
        public OrganizationalTaskStatusHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<bool> StatusExists(string statusName)
        {
            return await _context
                .OrganizationalTaskStatuses
                .AnyAsync(x => x.OtsName.Equals(statusName));
        }

        public async Task<int> GetStatusId(string statusName)
        {
            return await _context
                .OrganizationalTaskStatuses
                .Where(x => x.OtsName == statusName)
                .Select(x => x.OtsId)
                .SingleOrDefaultAsync();
        }
    }
}