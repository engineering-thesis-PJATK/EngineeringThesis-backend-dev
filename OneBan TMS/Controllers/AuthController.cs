using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Email;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailSender _emailSender;
        public AuthController(IUserRepository userRepository, IPasswordHandler passwordHandler, ITokenHandler tokenHandler, IEmployeeRepository employeeRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
            _employeeRepository = employeeRepository;
            _emailSender = emailSender;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody]CredentialsDto request)
        {
            var systemUser = await _userRepository.GetUserByEmail(request.Email);
            byte[] passwordHash;
            byte[] passwordSalt;
            if (systemUser is null)
            {
                return BadRequest("User not found");
            }
            _userRepository.GetPasswordParts(systemUser.EmpPassword, out passwordHash, out passwordSalt);
            if(!_passwordHandler.VerifyPasswordHash(request.Password, passwordHash, passwordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = _tokenHandler.CreateToken(systemUser.EmpEmail, await _userRepository.GetUserRole(systemUser.EmpEmail));
            return Ok(token);
        }

        [HttpPost("{employeeId}/Roles")]
        public async Task<IActionResult> AddRolesForEmployee(int employeeId, [FromBody] List<int> employeePriviles)
        {
            if (!(await _employeeRepository.ExistsEmployee(employeeId)))
                return BadRequest($"There is no employee with Id {employeeId}");
            if (!(await _employeeRepository.ExistsEmployeePrivileges(employeePriviles)))
                return BadRequest("One of privileges not exisits");
            await _userRepository.AddPrivilegesToUser(employeeId, employeePriviles);
            return Ok("Added privileges to employee");
            //Todo: Walidacja w sytuacji jak użytkownik posiada już role
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailAddress)
        {
            //Todo: Sprawdzić jak baza zacznie działać
            var employeeExists = await _employeeRepository
                .ExistsEmployeeByEmail(emailAddress);
            if (employeeExists)
            {
                string randomPassword = await _employeeRepository.ChangePassword(emailAddress);
                var message = new Message(new string[] {emailAddress}, "Nowe hasło", $"Twoje nowe hasło to {randomPassword}");
                _emailSender.SendEmail(message);
            }

            return Ok("Message");
        }
        
    }
}
