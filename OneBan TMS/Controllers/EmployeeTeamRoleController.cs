using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                return NotFound();
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
            await _employeeTeamRoleRepository.AddNewEmployeeTeamRole(employeeTeamRoleDto);
            return Ok("Added new employee team role");
        }

        [HttpPut("{employeeTeamRoleId}")]
        public async Task<IActionResult> UpdateEmployeeTeamRole([FromBody] EmployeeTeamRoleDto employeeTeamRoleDto, int employeeTeamRoleId)
        {
            await _employeeTeamRoleRepository.UpdateEmployeeTeamRole(employeeTeamRoleDto, employeeTeamRoleId);
            return Ok("Employee team role updated");
        }

        [HttpDelete("{employeeTeamRoleId}")]
        public async Task<IActionResult> DeleteEmployeeTeamRole(int employeeTeamRoleId)
        {
            await _employeeTeamRoleRepository.DeleteEmployeeTeamRole(employeeTeamRoleId);
            return Ok("Employee team role deleted");
        }
    }
}