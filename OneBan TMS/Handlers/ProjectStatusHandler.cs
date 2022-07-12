using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class ProjectStatusHandler : IProjectStatusHandler
    {
        private readonly OneManDbContext _context;
        public ProjectStatusHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsProjectStatusExist(int projectStatusId)
        {
            return await _context.ProjectStatuses.AnyAsync(x =>
                x.PjsId == projectStatusId);
        }
    }
}