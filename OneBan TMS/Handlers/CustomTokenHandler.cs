using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneBan_TMS.Handlers
{
    public class  CustomTokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        public CustomTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(string email, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString())
            };
            string tmp = _configuration.GetSection("AppSettings").GetSection("Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tmp));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
