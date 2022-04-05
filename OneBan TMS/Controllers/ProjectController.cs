using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProject Project{ get; init; }

        public ProjectController(IProject pProject)
        {
            Project = pProject;
        }
        
        [HttpGet("{projectId}")]
        public IActionResult GetProjectById(int projectId)
        {
            if (projectId < 1)
                return BadRequest();

            Project singleProject = Project.GetProjectById(projectId);
            if (singleProject is null)
                return NotFound();
            
            return Ok(singleProject);
        }
        
        [HttpGet()]
        public IActionResult GetAllProjectes()
        {
            var projectList = Project.GetAllProjects();
            if (projectList.Any())
                return Ok(projectList);
            else
                return NoContent();
        }
    }
}