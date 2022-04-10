using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeamRepository _teamRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, ITeamRepository teamRepository)
        {
            _employeeRepository = employeeRepository;
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesList()
        {
            IEnumerable<EmployeeDto> employeeList = _employeeRepository
                                                    .GetAllEmployeeDto();
            return 
                Ok(employeeList);
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int employeeId)
        {
            EmployeeDto employeeDto = _employeeRepository
                                      .GetEmployeeByIdDto(employeeId);
            return 
                Ok(employeeDto);
        }
        [HttpGet("Privelage")]
        public async Task<ActionResult<EmployeePrivilege>> GetAllEmployeePrivilages()
        {
            var privilages = await _employeeRepository.GetAllEmployeePrivilages();
            if (privilages is null)
                return 
                    BadRequest("No privelages assigned to employees");
            return 
                Ok(privilages);
        }

        [HttpGet("Team")]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teamList = await _teamRepository
                .GetTeams();
            if (teamList is null)
            {
                return BadRequest();
            }
            if (!(teamList.Any()))
            {
                return 
                    NoContent();
            }
            
            return 
                Ok(teamList);
        }

        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult<Team>> GetTeamById(int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest("Team id must be greater than 0");
            }
            Team singleTeam = await _teamRepository
                                    .GetTeamById(teamId);
            if (singleTeam is null)
            {
                return 
                    NotFound($"No team with id: {teamId} found");
            }
            
            return 
                Ok(singleTeam);
        }

        [HttpDelete("Team/{teamId}")]
        public async Task<ActionResult> DeleteTeamById(int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest("Team id must be greater than 0");
            }

            await _teamRepository
                  .DeleteTeamById(teamId);
            return
                Ok($"Team with id {teamId} has been deleted");
        }
        
    }
}