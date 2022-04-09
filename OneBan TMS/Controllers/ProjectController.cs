using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectRepository _projectRepository{ get; init; }

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int projectId)
        {
            if (projectId < 1)
            {
                return 
                    BadRequest("Project id must be greater than 0");
            }
            ProjectDto singleProject = await _projectRepository
                .GetProjectById(projectId);
            if (singleProject is null)
            {
                return NotFound($"No ticket with id: {projectId} found");
            }
            
            return Ok(singleProject);
        }
        
        [HttpGet()]
        public IActionResult GetProjects()
        {
            var projectList = _projectRepository.GetProjects();
            if (projectList.Any())
                return Ok(projectList);
            else
                return NoContent();
        }
    }
}