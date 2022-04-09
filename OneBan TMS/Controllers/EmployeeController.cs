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
        private readonly OneManDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(OneManDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesList()
        {
            return Ok(await _employeeRepository.GetAllEmployeeDto());
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int employeeId)
        {
            return Ok(await _employeeRepository.GetEmployeeByIdDto(employeeId));
        }
        [HttpGet("privilages")]
        public async Task<ActionResult<EmployeePrivilege>> GetAllEmployeePrivilages()
        {
            var privilages = await _employeeRepository.GetAllEmployeePrivilages();
            if (privilages is null)
                return BadRequest("List of employee privilages is empty");
            return Ok(privilages);
        }
    }
}