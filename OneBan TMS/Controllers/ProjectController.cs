using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly OneManDbContext _Context;
        private IProjectRepository _ProjectRepository;

        public ProjectController(OneManDbContext pContext, IProjectRepository pProjectRepository)
        {
            _Context = pContext;
            _ProjectRepository = pProjectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectes()
        {
            IEnumerable<Project> projectList = await _ProjectRepository.GetProjects();

            if (projectList.Any())
            {
                return Ok(projectList);
            }
            else
            {
                return NotFound();
            }
        }


        
        [HttpGet("{idProject}")]
        public async Task<IActionResult> GetProjectById(int pIdProject)
        {
            if (pIdProject < 1)
            {
                return BadRequest();
            }

            Project project = await _ProjectRepository.GetProjectById(pIdProject);

            if (project == null)
            {
                return NotFound();
            }
            
            return Ok(project);
        }
    }
}