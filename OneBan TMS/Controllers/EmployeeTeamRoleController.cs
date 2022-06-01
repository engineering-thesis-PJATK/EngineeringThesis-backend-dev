using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTeamRoleController : Controller
    {
        private readonly IEmployeeTeamRoleRepository _employeeTeamRoleRepository;
        private readonly IValidator<EmployeeTeamRoleDto> _employeeTeamRoleValidator;
        public EmployeeTeamRoleController(IEmployeeTeamRoleRepository employeeTeamRoleRepository, IValidator<EmployeeTeamRoleDto> employeeTeamRoleValidator)
        {
            _employeeTeamRoleRepository = employeeTeamRoleRepository;
            _employeeTeamRoleValidator = employeeTeamRoleValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeTeamRoles()
        {
            IEnumerable<EmployeeTeamRole> employeeTeamRoles = await _employeeTeamRoleRepository.GetEmployeeTeamRoles();
            if (employeeTeamRoles is null)
                return NoContent();
            return Ok(employeeTeamRoles);
        }

        [HttpGet("{employeeTeamRoleId}")]
        public async Task<IActionResult> GetEmployeeTeamRolesById(int employeeTeamRoleId)
        {
            EmployeeTeamRole employeeTeamRole = await _employeeTeamRoleRepository.GetEmployeeTeamRoleById(employeeTeamRoleId);
            if (employeeTeamRole is null)
                return NoContent();
            return Ok(employeeTeamRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeTeamRole([FromBody]EmployeeTeamRoleDto employeeTeamRoleDto)
        {
            var validatorResult = await _employeeTeamRoleValidator.ValidateAsync(employeeTeamRoleDto);
            if (!(validatorResult.IsValid))
                return BadRequest(MessageHelper.GetBadRequestMessage(
                    validatorResult.Errors[0].ErrorMessage,
                    validatorResult.Errors[0].PropertyName));
            var employeeTeamRole = await _employeeTeamRoleRepository
                .AddNewEmployeeTeamRole(employeeTeamRoleDto);
            return Ok(MessageHelper
                .GetSuccessfulMessage("Added successfully new employee team role", null, employeeTeamRole.EtrId));
        }

        [HttpPut("{employeeTeamRoleId}")]
        public async Task<IActionResult> UpdateEmployeeTeamRole([FromBody] EmployeeTeamRoleDto employeeTeamRoleDto, int employeeTeamRoleId)
        {
            if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(employeeTeamRoleId)))
                return BadRequest(MessageHelper.GetBadRequestMessage("Employee team role does not exists"));
            var validatorResult = await _employeeTeamRoleValidator.ValidateAsync(employeeTeamRoleDto);
            if (!(validatorResult.IsValid))
                return BadRequest(MessageHelper.GetBadRequestMessage(
                    validatorResult.Errors[0].ErrorMessage,
                    validatorResult.Errors[0].PropertyName));
            await _employeeTeamRoleRepository.UpdateEmployeeTeamRole(employeeTeamRoleDto, employeeTeamRoleId);
            return Ok(MessageHelper
                .GetSuccessfulMessage("Updated successfully employee team role"));
        }

        [HttpDelete("{employeeTeamRoleId}")]
        public async Task<IActionResult> DeleteEmployeeTeamRole(int employeeTeamRoleId)
        {
            if (!(await _employeeTeamRoleRepository.ExistsEmployeeTeamRole(employeeTeamRoleId)))
                return BadRequest(MessageHelper.GetBadRequestMessage("Employee team role does not exists"));
            await _employeeTeamRoleRepository
                .DeleteEmployeeTeamRole(employeeTeamRoleId);
            return Ok(MessageHelper
                .GetSuccessfulMessage("Deleted successfully Employee team role"));
        }
    }
}