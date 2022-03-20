using OneBan_TMS.Enum;
using OneBan_TMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string emailAddress);
        void AddNewUser(UserDto user, byte[] PasswordHash, byte[] PasswordSalt);
        Roles? GetUserRole(string role);
    }
}
