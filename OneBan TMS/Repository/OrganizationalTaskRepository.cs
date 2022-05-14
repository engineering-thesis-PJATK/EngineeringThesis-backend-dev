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
        public async Task<List<KanbanElement>> GetTaskForEmployee(int statusId, int employeeId)
        {
            List<KanbanElement> kanbanElements = new List<KanbanElement>();
            var tasks = await _context
                .OrganizationalTasks
                .Where(x => x.OtkIdEmployee == employeeId 
                            && x.OtkIdOrganizationalTaskStatus == statusId)
                .ToListAsync();
            foreach (var task in tasks)
            {
                kanbanElements.Add(new KanbanElement()
                {
                    Id = task.OtkId,
                    Topic = task.OtkDescription,
                    Type = (int)KanbanType.Task
                });
            }

            return kanbanElements;
        }

        public Task UpdateTaskStatus(int taskId, int statusId)
        {
            //Todo: implementacja
            throw new System.NotImplementedException();
        }

        public async Task<int> GetTaskStatusId(string status)
        {
            var result = await _context
                .OrganizationalTaskStatuses
                .Where(x => x.OtsName.Equals(status))
                .Select(x => x.OtsId)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}