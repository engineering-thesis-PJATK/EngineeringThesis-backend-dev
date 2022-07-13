using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository _projectRepository{ get; init; }
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectStatusHandler _projectStatusHandler;
        public ProjectController(IProjectRepository projectRepository, IProjectTaskRepository projectTaskRepository, ICompanyRepository companyRepository, ITeamRepository teamRepository, IProjectStatusHandler projectStatusHandler)
        {
            _projectRepository = projectRepository;
            _projectTaskRepository = projectTaskRepository;
            _companyRepository = companyRepository;
            _teamRepository = teamRepository;
            _projectStatusHandler = projectStatusHandler;
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

        [HttpGet("CompanyTeamNames")]
        public async Task<ActionResult<IEnumerable<ProjectCompanyTeamNamesDto>>> GetProjectsWithCompanyNameTeamName()
        {
            var projectCompanyTeamNamesDtoList = await _projectRepository.GetProjectsWithCompanyNameTeamName();
            if (!(projectCompanyTeamNamesDtoList.Any()))
                return NoContent();
            return Ok(projectCompanyTeamNamesDtoList);
        }

        [HttpGet("CompanyTeamNames/{projectId}")]
        public async Task<ActionResult<ProjectCompanyTeamNamesDto>> GetProjectWithCompanyNameTeamNameById(int projectId)
        {
            var projectCompanyTeamNamesDto = await _projectRepository.GetProjectWithCompanyNameTeamNameById(projectId);
            if (projectCompanyTeamNamesDto is null)
                return NoContent();
            return Ok(projectCompanyTeamNamesDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProject([FromBody] ProjectNewDto projectNewDto)
        {
            if (!(await _companyRepository.IsCompanyExists(projectNewDto.ProIdCompany)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Company does not exist"));
            if(!(await _teamRepository.ExistsTeam(projectNewDto.ProIdTeam)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Team does not exist"));
            if(!(await _projectStatusHandler.IsProjectStatusExist(projectNewDto.ProStatusId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Project status does not exist"));
            var addedProject = await _projectRepository.AddNewProject(projectNewDto);
            return Ok(MessageProvider.GetSuccessfulMessage("Project added successfully", null, addedProject.ProId));
        }
        [HttpGet("{projectId}/ProjectTasks")]
        public async Task<IActionResult> GetProjectTasksForProject(int projectId)
        {
            IEnumerable<ProjectTask> projectTasks = await _projectTaskRepository
                .GetProjectTasksForProject(projectId);
            if (!(projectTasks.Any()))
                return NoContent();
            return Ok(projectTasks);
        }

        [HttpGet("/ProjectTask/{projectTaskId}")]
        public async Task<IActionResult> GetProjectTaskById(int projectTaskId)
        {
            ProjectTask projectTask = await _projectTaskRepository
                .GetProjectTaskById(projectTaskId);
            if (projectTask is null)
                return NoContent();
            return Ok(projectTask);
        }
        [HttpPost("{projectId}/ProjectTask")]
        public async Task<IActionResult> AddNewProjectTaskForProject(int projectId, [FromBody]ProjectTaskDto projectTaskDto)
        {
            //Todo: Walidacja i poprawa
            var projectTask = await _projectTaskRepository.AddNewProjectTask(projectTaskDto, projectId);
            return Ok(MessageProvider.GetSuccessfulMessage("Added successfully project task", null, projectTask.PtkId));
        }

        [HttpPut("{projectId}/ProjectTask")]
        public async Task<IActionResult> UpdateProjectTaskForProject(int projectId, [FromBody] ProjectTaskDto projectTaskDto)
        {
            //Todo: walidacja i poprawa
            await _projectTaskRepository.UpdateProjectTask(projectTaskDto, projectId);
            return Ok(MessageProvider.GetBadRequestMessage("Updated successfully project task"));
        }

        [HttpDelete("/ProjectTask/{projectId}")]

        public async Task<IActionResult> DeleteProjectTaskForProject(int projectId)
        {
            if (!(await _projectTaskRepository.ExistsProjectTask(projectId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Project task does not exist"));
            await _projectTaskRepository.DeleteProjectTask(projectId);
            return Ok(MessageProvider.GetSuccessfulMessage("Deleted successfully project task"));
        }
    }
}