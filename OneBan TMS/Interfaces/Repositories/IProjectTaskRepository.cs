using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetProjectTasks();
        Task<IEnumerable<ProjectTask>> GetProjectTasksForProject(int projectId);
        Task<ProjectTask> GetProjectTaskById(int projectTaskId);
        Task<ProjectTask> AddNewProjectTask(ProjectTaskDto projectTaskDto, int projectId);
        Task UpdateProjectTask(ProjectTaskDto projectTaskDto, int projectTaskId);
        Task DeleteProjectTask(int projectTaskId);
    }
}