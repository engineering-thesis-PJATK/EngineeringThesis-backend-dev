using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Kanban;

namespace OneBan_TMS.Repository
{
    public class OrganizationalTaskRepository : IOrganizationalTaskRepository
    {
        private readonly OneManDbContext _context;
        private readonly IOrganizationalTaskStatusHandler _taskStatusHandler;
        public OrganizationalTaskRepository(OneManDbContext context, IOrganizationalTaskStatusHandler taskStatusHandler)
        {
            _context = context;
            _taskStatusHandler = taskStatusHandler;
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

        public async Task UpdateTaskStatus(int taskId, int statusId)
        {
            var task = await _context
                .OrganizationalTasks
                .SingleOrDefaultAsync(x => x.OtkId == taskId);
            if (task is null)
                throw new ArgumentException("Task not exists");
            if (await _taskStatusHandler.StatusExists(statusId))
                throw new ArgumentException("Status not exists");
            
        }
    }
}