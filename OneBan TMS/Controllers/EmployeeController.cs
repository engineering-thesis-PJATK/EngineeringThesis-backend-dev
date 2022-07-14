using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Models.DTOs.Messages;
using OneBan_TMS.Models.DTOs.Team;
using OneBan_TMS.Models.DTOs.TeamMember;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IEmployeeTeamRoleRepository _employeeTeamRoleRepository;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IValidator<EmployeeToUpdateDto> _validatorEmployeeToUpdate;
        private readonly IValidator<EmployeeDto> _validatorEmployeeDto;
        public EmployeeController(IEmployeeRepository employeeRepository, ITeamRepository teamRepository, IEmployeeTeamRoleRepository employeeTeamRoleRepository, ITeamMemberRepository teamMemberRepository, IValidator<EmployeeToUpdateDto> validatorEmployeeToUpdate, IValidator<EmployeeDto> validatorEmployeeDto)
        {
            _employeeRepository = employeeRepository;
            _teamRepository = teamRepository;
            _employeeTeamRoleRepository = employeeTeamRoleRepository;
            _validatorEmployeeToUpdate = validatorEmployeeToUpdate;
            _validatorEmployeeDto = validatorEmployeeDto;
            _teamMemberRepository = teamMemberRepository;
        }
        #region Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeForListDto>>> GetEmployees()
        {
            var employee = await _employeeRepository.GetAllEmployeeDto();
            if (!(employee.Any()))
                return NoContent();
            return Ok(employee);
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeForListDto>> GetEmployeeById(int employeeId)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return NoContent();
            EmployeeForListDto employeeDto = await  _employeeRepository
                .GetEmployeeByIdDto(employeeId);
            return Ok(employeeDto);
        }

        [HttpGet("/Short")]
        public async Task<ActionResult> GetEmployeeShort()
        {
            var employeeShortDtoList = await _employeeRepository.GetEmployeeShortDto();
            if (employeeShortDtoList.Any())
                return NoContent();
            return Ok(employeeShortDtoList);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewEmployee([FromBody]EmployeeDto newEmployee)
        {
            var validatorEmployeeDtoResult = await _validatorEmployeeDto.ValidateAsync(newEmployee);
            if (!(validatorEmployeeDtoResult.IsValid))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage(
                    validatorEmployeeDtoResult.Errors[0].ErrorMessage,
                    validatorEmployeeDtoResult.Errors[0].PropertyName));
            }
            var employee = await _employeeRepository.AddEmployee(newEmployee);
            return Ok(MessageProvider.GetSuccessfulMessage("Added successfully employee", null, employee.EmpId));
        }
        [HttpPost("{employeeId}/Roles")]
        public async Task<IActionResult> ChangeEmployeePrivileges(int employeeId, [FromBody] List<int> employeePriviles)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("User does not exists"));
            if (!(await _employeeRepository.ExistsEmployeePrivileges(employeePriviles)))
                return BadRequest(MessageProvider.GetBadRequestMessage("One of privileges do not exist"));
            await _employeeRepository.ChangePrivilegesToUser(employeeId, employeePriviles);
            return Ok(MessageProvider.GetSuccessfulMessage("Added successfully privileges to employee"));
        }
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeToUpdateDto employeeToUpdate)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage("Employee does not exists"));
            }
            var validatorEmployeeToUpdateResult = await _validatorEmployeeToUpdate.ValidateAsync(employeeToUpdate);
            if (!(validatorEmployeeToUpdateResult.IsValid))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage(
                    validatorEmployeeToUpdateResult.Errors[0].ErrorMessage,
                    validatorEmployeeToUpdateResult.Errors[0].PropertyName
                ));
            }
            await _employeeRepository.UpdateEmployee(employeeId, employeeToUpdate);
            return Ok(MessageProvider.GetSuccessfulMessage("Updated successfully employee"));
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Employee does not exist"));
            await _employeeRepository.DeleteEmployee(employeeId);
            return Ok(MessageProvider.GetSuccessfulMessage("Deleted employee successfully"));
        }
        [HttpPost("Team/EmployeeRole")]
        public async Task<IActionResult> AddEmployeeWithRoleToTeam([FromBody]EmployeeWithRoleToTeamDto employeeWithRoleToTeamDto)
        {
            if (!(await _teamRepository.ExistsTeam(employeeWithRoleToTeamDto.TeamId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Team does not exists"));
            foreach (var employeeWithRole in employeeWithRoleToTeamDto.EmployeesWithRoles)
            {
                if (!(await _employeeRepository.ExistsEmployee(employeeWithRole.employeeId)))
                    return BadRequest(MessageProvider.GetBadRequestMessage($"Employee with ID {employeeWithRole.employeeId} does not exist"));
                if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(employeeWithRole.teamRoleId)))
                    return BadRequest(
                        MessageProvider.GetBadRequestMessage($"Employee team role with ID {employeeWithRole.teamRoleId} does not exist"));
            }
            await _teamRepository.AddEmployeesToTeamWithRoles(employeeWithRoleToTeamDto);
            return Ok(MessageProvider.GetSuccessfulMessage("Employee with role added to team successfully"));
        }
        #endregion
        
        #region EmployeeTeamMember
        [HttpPost("Team/TeamMember")]
        public async Task<IActionResult> AddTeamMember([FromBody]TeamMemberAddDto newTeamMemberAddDto)
        {
            if(!(await _teamRepository.ExistsTeam(newTeamMemberAddDto.TmrIdTeam)))
                return BadRequest(MessageProvider.GetBadRequestMessage($"Team with id {newTeamMemberAddDto.TmrIdTeam} does not exist"));
            if (!(await _employeeRepository.ExistsEmployee(newTeamMemberAddDto.TmrIdEmployee)))
                return BadRequest(
                    MessageProvider.GetBadRequestMessage($"Employee with id {newTeamMemberAddDto.TmrIdEmployee} does not exist"));
            if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(newTeamMemberAddDto.TmrIdRole)))
                return BadRequest(
                    MessageProvider.GetBadRequestMessage($"Employee team role with id {newTeamMemberAddDto.TmrIdRole} does not exist"));
            await _teamMemberRepository.AddTeamMember(newTeamMemberAddDto);
            return Ok(MessageProvider.GetSuccessfulMessage("Team member added successfully"));
        }
        [HttpDelete("Team/TeamMember")]
        public async Task<IActionResult> DeleteTeamMember(int teamId, int employeeId, int roleId)
        {
            if(!(await _teamMemberRepository.ExistsTeamsMember(teamId, employeeId, roleId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Team member does not exist"));
            await DeleteTeamMember(teamId, employeeId, roleId);
            return Ok(MessageProvider.GetSuccessfulMessage("Team member deleted successfully"));
        }
        [HttpPatch("Team/Member")]
        public async Task<IActionResult> UpdateTeamMemberRole(int teamId, int employeeId, int newRoleId)
        {
            if(!(await _teamRepository.ExistsTeam(teamId)))
                return BadRequest(MessageProvider.GetBadRequestMessage($"Team with id {teamId} does not exist"));
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest(
                    MessageProvider.GetBadRequestMessage($"Employee with id {employeeId} does not exist"));
            if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(newRoleId)))
                return BadRequest(
                    MessageProvider.GetBadRequestMessage($"Employee team role with id {newRoleId} does not exist"));
            await _teamMemberRepository.UpdateTeamMemberRole(teamId, employeeId, newRoleId);
            return Ok(MessageProvider.GetSuccessfulMessage("Role updated successfully"));
        }
        #endregion
        
        #region EmployeeTeam
        #region GetById
        [HttpGet("Team/{teamId}")]
        public async Task<ActionResult<TeamGetDto>> GetTeamById(int teamId)
        {
            if (teamId < 1)
            {
                return BadRequest(MessageProvider.GetBadRequestMessage("Team id must be greater than 0"));
            }
            var singleTeam = await _teamRepository
                .GetTeamById(teamId);
            if (singleTeam is null)
            {
                return 
                    BadRequest(MessageProvider.GetBadRequestMessage($"Team does not exist"));
            }
            return 
                Ok(singleTeam);
        }
        [HttpGet("Privilege/{privilegeId}")]
        public async Task<ActionResult<EmployeePrivilege>> GetEmployeePrivilegeById(int privilegeId)
        {
            if (privilegeId < 1)
            {
                return 
                    BadRequest(MessageProvider.GetBadRequestMessage("Privilege id must be greater than 0"));
            }

            var singlePrivilege = await _employeeRepository
                                                       .GetEmployeePrivilegeById(privilegeId);
            if (singlePrivilege is null)
            {
                return 
                    BadRequest(MessageProvider.GetBadRequestMessage($"Privilego does not exist"));
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
                return BadRequest(MessageProvider.GetBadRequestMessage("No privileges assigned to employees"));
            
            return Ok(privileges);
        }
        [HttpGet("Team")]
        public async Task<ActionResult<List<Team>>> GetTeams()
        {
            var teamList = await _teamRepository
                                 .GetTeams();
            if (teamList is null || !(teamList.Any()))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage("Teams does not exist"));
            }

            return 
                Ok(teamList);
        }
        #endregion
        
        #region Post
        [HttpPost("Team")]
        public async Task<ActionResult<TeamGetDto>> PostTeam([FromBody]TeamUpdateDto newTeam)
        {
            if (ModelState.IsValid)
            {
                var team = await _teamRepository.PostTeam(newTeam);
                return Ok(MessageProvider.GetSuccessfulMessage("Added successfully team", null, team.TemId));
            }

            return
                BadRequest(MessageProvider.GetBadRequestMessage("Team has not been added"));
        }
        #endregion
        
        #region Put

        [HttpPut("Team/{teamId}")]
        public async Task<ActionResult<Team>> UpdateTeamById(int teamId,TeamUpdateDto teamGetUpdateDto)
        {
            if (ModelState.IsValid)
            {
                if (teamGetUpdateDto is null)
                {
                    return 
                        BadRequest(MessageProvider.GetBadRequestMessage("Team cannot be empty"));
                }
                if (teamId < 1)
                {
                    return 
                        BadRequest(MessageProvider.GetBadRequestMessage("Team id must be greater than 0"));
                }
                if (!(await _teamRepository.ExistsTeam(teamId)))
                {
                    return BadRequest(MessageProvider.GetBadRequestMessage("Team does not exist"));
                }

                var singleTeam = await _teamRepository
                    .UpdateTeamById(teamId, teamGetUpdateDto);
                if (singleTeam is not null)
                {
                    return
                        Ok(MessageProvider.GetSuccessfulMessage("Updated successfully team"));
                }
            }

            return 
                BadRequest(MessageProvider.GetBadRequestMessage("Team was not updated"));
        }
        #endregion
        
        #region Delete

        [HttpDelete("Team/{teamId}")]
        public async Task<ActionResult> DeleteTeamById(int teamId)
        {
            if (teamId < 1)
            {
                return 
                    BadRequest(MessageProvider.GetBadRequestMessage("Team id must be greater than 0"));
            }

            if (!(await _teamRepository.ExistsTeam(teamId)))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage("Team does not exist"));
            }

            await _teamRepository
                .DeleteTeamById(teamId);
            return 
                Ok(MessageProvider.GetSuccessfulMessage("Deleted team successfully"));
        }
        #endregion
        #endregion

    }
}