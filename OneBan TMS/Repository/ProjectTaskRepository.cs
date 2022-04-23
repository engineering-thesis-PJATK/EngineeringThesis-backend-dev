using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;

namespace OneBan_TMS.Repository
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly OneManDbContext _context;
        public ProjectTaskRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProjectTask>> GetProjectTasks()
        {
            List<ProjectTask> projectTasks = await _context
                .ProjectTasks
                .ToListAsync();
            return projectTasks;
        }

        public async Task<IEnumerable<ProjectTask>> GetProjectTasksForProject(int projectId)
        {
            List<ProjectTask> projectTasks = await _context
                .ProjectTasks
                .Where(x => x.PtkIdProject == projectId)
                .ToListAsync();
            return projectTasks;
        }

        public async Task<ProjectTask> GetProjectTaskById(int projectTaskId)
        {
            ProjectTask projectTask = await _context
                .ProjectTasks
                .Where(x => x.PtkIdProject == projectTaskId)
                .SingleOrDefaultAsync();
            return projectTask;
        }
        public async Task AddNewProjectTask(ProjectTaskDto projectTaskDto, int projectId)
        {
            ProjectTask newProjectTask = new ProjectTask()
            {
                PtkContent = projectTaskDto.Content,
                PtkEstimatedCost = projectTaskDto.EstimatedCost,
                PtkIdProject = projectId
            };
            _context.ProjectTasks.Add(newProjectTask);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectTask(ProjectTaskDto projectTaskDto, int projectTaskId)
        {
            ProjectTask projectTaskToUpdate = await _context
                .ProjectTasks
                .Where(x => x.PtkId == projectTaskId)
                .SingleOrDefaultAsync();
            if (!(projectTaskToUpdate is null))
            {
                projectTaskToUpdate.PtkContent = projectTaskDto.Content;
                projectTaskToUpdate.PtkEstimatedCost = projectTaskDto.EstimatedCost;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProjectTask(int projectTaskId)
        {
            ProjectTask projectTaskToDelete = await _context
                .ProjectTasks
                .Where(x => x.PtkId == projectTaskId)
                .SingleOrDefaultAsync();
            if (!(projectTaskToDelete is null))
            {
                _context.ProjectTasks.Remove(projectTaskToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}