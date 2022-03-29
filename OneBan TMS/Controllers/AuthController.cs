using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OneBan_TMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IConfiguration configuration, IUserRepository userRepository, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody]UserDto userDto)
        {
            _passwordHandler.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            await _userRepository.AddNewUser(userDto, passwordHash, passwordSalt);
            return Ok(userDto);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]CredentialsDto request)
        {
            /*var user = _userRepository.GetUserByEmail(request.Email);
            if (user is null)
            {
                return BadRequest("User not found");
            }
            if(!_passwordHandler.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = _tokenHandler.CreateToken(user.Email, (Roles)_userRepository.GetUserRole(user.Role));
            return Ok(token);
            */
            return Ok();
        }
    }
}
