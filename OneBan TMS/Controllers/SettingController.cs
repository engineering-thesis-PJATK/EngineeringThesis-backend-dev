using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Setting;

namespace OneBan_TMS.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class SettingController : Controller
    {
        private readonly ISettingRepository _settingRepository;
        public SettingController(OneManDbContext context, ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        [HttpGet("/users/roles")]
        public async Task<ActionResult<List<UserPrivileges>>> GetUsersWithRoles()
        {
            var usersWithRoles = await _settingRepository.GetUserWithPrivileges();
            return Ok(usersWithRoles);
        }
    }
}