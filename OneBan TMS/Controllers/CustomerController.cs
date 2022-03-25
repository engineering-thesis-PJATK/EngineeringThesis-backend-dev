using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly OneManDbContext _context;
        public CustomerController(OneManDbContext context)
        {
            _context = context;
        }

        [HttpGet("customers")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersList()
        {
            return Ok(null);
        }
    }
}