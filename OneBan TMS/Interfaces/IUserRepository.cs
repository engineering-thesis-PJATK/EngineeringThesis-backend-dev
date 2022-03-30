using OneBan_TMS.Enum;
using OneBan_TMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface IUserRepository
    {
        Task<Employee> GetUserByEmail(string emailAddress);
        Task AddNewUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        void GetPasswordParts(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<string> GetUserRole(string userEmail);
        //Roles? GetUserRole(string role);
    }
}
