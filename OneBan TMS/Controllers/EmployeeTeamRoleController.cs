using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class EmployeeTeamRoleController : Controller
    {
        private readonly IEmployeeTeamRoleRepository _employeeTeamRoleRepository;
        public EmployeeTeamRoleController(IEmployeeTeamRoleRepository employeeTeamRoleRepository)
        {
            _employeeTeamRoleRepository = employeeTeamRoleRepository;
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
        public async Task<IActionResult> GetEmployeeTeamRolesById(int employeeTeamRoleById)
        {
            EmployeeTeamRole employeeTeamRole = await _employeeTeamRoleRepository.GetEmployeeTeamRoleById(employeeTeamRoleById);
            if (employeeTeamRole is null)
                return NotFound();
            return Ok(employeeTeamRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeTeamRole([FromBody]EmployeeTeamRoleDto employeeTeamRoleDto)
        {
            //Todo: Zrobić walidację
            var employeeTeamRole = await _employeeTeamRoleRepository.AddNewEmployeeTeamRole(employeeTeamRoleDto);
            return Ok(MessageHelper.GetSuccessfulMessage("Added successfully new employee team role", null, employeeTeamRole.EtrId));
        }

        [HttpPut("{employeeTeamRoleId}")]
        public async Task<IActionResult> UpdateEmployeeTeamRole([FromBody] EmployeeTeamRoleDto employeeTeamRoleDto, int employeeTeamRoleId)
        {
            //Todo: Zrobić walidację
            await _employeeTeamRoleRepository.UpdateEmployeeTeamRole(employeeTeamRoleDto, employeeTeamRoleId);
            return Ok(MessageHelper.GetSuccessfulMessage("Updated successfully employee team role"));
        }

        [HttpDelete("{employeeTeamRoleId}")]
        public async Task<IActionResult> DeleteEmployeeTeamRole(int employeeTeamRoleId)
        {
            //Todo: Zrobić walidację
            await _employeeTeamRoleRepository.DeleteEmployeeTeamRole(employeeTeamRoleId);
            return Ok(MessageHelper.GetSuccessfulMessage("Deleted successfully Employee team role"));
        }
    }
}