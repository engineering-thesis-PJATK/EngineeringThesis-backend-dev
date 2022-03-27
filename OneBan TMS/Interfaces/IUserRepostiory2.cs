using OneBan_TMS.Enum;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IUserRepostiory2
    {
        Employee GetUserByEmail(string emailAddress);
        void AddNewUser(UserDto user, byte[] passwordHash, byte[] passwordSalt);
        Roles? GetUserRole(string role);
    }
}