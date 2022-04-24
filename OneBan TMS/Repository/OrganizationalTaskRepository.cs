using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Repository
{
    public class OrganizationalTaskRepository : IOrganizationalTaskRepository
    {
        private readonly OneManDbContext _context;
        public OrganizationalTaskRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<List<KanbanElement>> GetTaskForEmployee(int employeeId)
        {
            List<KanbanElement> kanbanElements = new List<KanbanElement>();
            var tasks = await _context
                .OrganizationalTasks
                .Where(x => x.OtkIdEmployee == employeeId)
                .ToListAsync();
            foreach (var task in tasks)
            {
                kanbanElements.Add(new KanbanElement()
                {
                    Id = task.OtkId,
                    Topic = task.OtkDescription,
                    Type = KanbanType.Task
                });
            }

            return kanbanElements;
        }
    }
}