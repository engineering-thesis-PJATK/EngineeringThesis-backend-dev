using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;
using System.Threading.Tasks;

namespace OneBan_TMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IUserRepository userRepository, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody]UserDto userDto)
        {
            _passwordHandler.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            await _userRepository.AddNewUser(userDto, passwordHash, passwordSalt);
            return Ok(userDto);
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
    }
}
