using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;
using System.Threading.Tasks;
using Microsoft.Graph;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Models.DTOs.Messages;
using Message = OneBan_TMS.Models.DTOs.Email.Message;

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
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "User does not exists",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            _userRepository.GetPasswordParts(systemUser.EmpPassword, out passwordHash, out passwordSalt);
            if(!_passwordHandler.VerifyPasswordHash(request.Password, passwordHash, passwordSalt))
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Wrong password",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            string token = _tokenHandler.CreateToken(systemUser.EmpEmail, await _userRepository.GetUserRole(systemUser.EmpEmail));
            return Ok(new MessageResponse()
            {
                MessageContent = token,
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpGet("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailAddress)
        {
            var employeeExists = await _employeeRepository
                .ExistsEmployeeByEmail(emailAddress);
            if (employeeExists)
            {
                string randomPassword = await _employeeRepository.ChangePassword(emailAddress);
                var message = new Message(new string[] {emailAddress}, "Nowe hasło", $"Twoje nowe hasło to {randomPassword}");
                _emailSender.SendEmail(message);
            }

            return Ok(new MessageResponse()
            {
                MessageContent = "Message send to email",
                StatusCode = HttpStatusCode.OK
            });
        }
        
    }
}
