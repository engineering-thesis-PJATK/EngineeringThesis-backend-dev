using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Models.DTOs.Team;

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
        #region GetById
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeForListDto>> GetEmployeeById(int employeeId)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return NoContent();
            EmployeeForListDto employeeDto = await  _employeeRepository
                                                .GetEmployeeByIdDto(employeeId);
            return Ok(employeeDto);
        }

        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult<TeamGetDto>> GetTeamById(int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest("Team id must be greater than 0");
            }
            var singleTeam = await _teamRepository
                .GetTeamById(teamId);
            if (singleTeam is null)
            {
                return 
                    NotFound($"No team with id: {teamId} found");
            }
            return 
                Ok(singleTeam);
        }
        
        [HttpGet("Privilege/{privilegeId}")]
        public async Task<ActionResult<EmployeePrivilege>> GetEmployeePrivilegeById(int privilegeId)
        {
            if (privilegeId < 1)
            {
                return BadRequest("Privilege id must be greater than 0");
            }

            var singlePrivilege = await _employeeRepository
                                                       .GetEmployeePrivilegeById(privilegeId);
            if (singlePrivilege is null)
            {
                return 
                    NotFound($"No privilege with id: {privilegeId} found");
            }
            
            return 
                Ok(singlePrivilege);
        }
        
        #endregion     
        #region getList
        [HttpGet("Privilege")]
        public async Task<ActionResult<EmployeePrivilege>> GetEmployeePrivileges()
        {
            var privileges = await _employeeRepository
                                                      .GetEmployeePrivileges();
            if (privileges is null)
                return BadRequest("No privileges assigned to employees");
            return Ok(privileges);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeForListDto>>> GetEmployees()
        {
            var employee = await _employeeRepository.GetAllEmployeeDto();
            if (!(employee.Any()))
                return NoContent();
            return Ok(employee);
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
        #endregion
        #region Post

        [HttpPost]
        public async Task<ActionResult> AddNewEmployee([FromBody]EmployeeDto employee)
        {
            if (await _employeeRepository.ExistsEmployeeByEmail(employee.EmpEmail))
                return BadRequest("There is an employee with the specified email");
            await _employeeRepository.AddEmployee(employee);
            return Ok("Employee added successful");
        }
        [HttpPost("Team")]
        public async Task<ActionResult<TeamGetDto>> PostTeam(TeamUpdateDto newTeam)
        {
            if (ModelState.IsValid)
            {
                return
                    await _teamRepository.PostTeam(newTeam);
            }

            return
                BadRequest();
        }
        #endregion
        #region Put

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeToUpdate employeeToUpdate)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return NoContent();
            await _employeeRepository.UpdateEmployee(employeeId, employeeToUpdate);
            return Ok("Employee updated");
        }

        [HttpPut("Team/{teamId}")]
        public async Task<ActionResult<Team>> UpdateTeamById(int teamId,TeamUpdateDto teamGetUpdateDto)
        {
            if (ModelState.IsValid)
            {
                if (teamGetUpdateDto is null)
                {
                    return 
                        BadRequest("Team cannot be empty");
                }
                if (teamId < 1)
                {
                    return 
                        BadRequest("Team id must be greater than 0");
                }

                var singleTeam = await _teamRepository
                    .UpdateTeamById(teamId, teamGetUpdateDto);
                if (singleTeam is not null)
                {
                    return
                        Ok(singleTeam);
                }
            }

            return 
                BadRequest("Operation was not executed");
        }
        #endregion
        #region Delete
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
        #endregion

    }
}