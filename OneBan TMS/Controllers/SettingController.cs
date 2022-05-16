using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class SettingController
    {
        private readonly OneManDbContext _context;
        public SettingController(OneManDbContext context)
        {
            _context = context;
        }

        [HttpGet("/users/roles")]
        public async Task<IActionResult<>> GetUsersWithRoles()
        {
            
        }
    }
}