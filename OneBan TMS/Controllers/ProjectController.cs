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
        
        [HttpGet("GetProjectById")]
        public IActionResult GetProjectById(int pProjectId)
        {
            if (pProjectId < 1)
                return BadRequest();

            Project singleProject = Project.GetProjectById(pProjectId);
            if (singleProject is null)
                return NotFound();
            
            return Ok(singleProject);
        }
        
        [HttpGet("GetAllProjectes")]
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