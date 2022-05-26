using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;

namespace OneBan_TMS.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository _projectRepository{ get; init; }
        private readonly IProjectTaskRepository _projectTaskRepository;
        public ProjectController(IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository)
        {
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
        }
        
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int projectId)
        {
            if (projectId < 1)
            {
                return 
                    BadRequest("Project id must be greater than 0");
            }
            Project singleProject = await _projectRepository
                                          .GetProjectById(projectId);
            if (singleProject is null)
            {
                return NotFound($"No ticket with id: {projectId} found");
            }
            
            return Ok(singleProject);
        }
        
        [HttpGet()]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            var projectList = await _projectRepository
                                    .GetProjects();
            if (projectList is null)
            {
                return BadRequest();
            }
            if (!(projectList.Any()))
            {
                return NoContent();
            }
            
            return Ok(projectList);
        }

        [HttpGet("{projectId}/ProjectTasks")]
        public async Task<IActionResult> GetProjectTasksForProject(int projectId)
        {
            IEnumerable<ProjectTask> projectTasks = await _projectTaskRepository.GetProjectTasksForProject(projectId);
            if (projectTasks is null)
                return NotFound();
            return Ok(projectTasks);
        }

        [HttpGet("/ProjectTask/{projectTaskId}")]
        public async Task<IActionResult> GetProjectTaskById(int projectTaskId)
        {
            ProjectTask projectTask = await _projectTaskRepository.GetProjectTaskById(projectTaskId);
            if (projectTask is null)
                return NotFound();
            return Ok(projectTask);
        }
        [HttpPost("{projectId}/ProjectTask")]
        public async Task<IActionResult> AddNewProjectTaskForProject(int projectId, [FromBody] ProjectTaskDto projectTaskDto)
        {
            //Todo: Walidacja
            var projectTask = await _projectTaskRepository.AddNewProjectTask(projectTaskDto, projectId);
            return Ok(MessageHelper.GetSuccessfulMessage("Added successfully project task", null, projectTask.PtkId));
        }

        [HttpPut("{projectId}/ProjectTask")]
        public async Task<IActionResult> UpdateProjectTaskForProject(int projectId, [FromBody] ProjectTaskDto projectTaskDto)
        {
            //Todo: walidacja
            await _projectTaskRepository.UpdateProjectTask(projectTaskDto, projectId);
            return Ok(MessageHelper.GetBadRequestMessage("Updated successfully project task"));
        }

        [HttpDelete("/ProjectTask/{projectId}")]

        public async Task<IActionResult> DeleteProjectTaskForProject(int projectId)
        {
            //Walidacja
            await _projectTaskRepository.DeleteProjectTask(projectId);
            return Ok(MessageHelper.GetSuccessfulMessage("Deleted successfully project task"));
        }
    }
}