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
        Task AddNewUser(UserDto user, byte[] PasswordHash, byte[] PasswordSalt);

        //Task<EmployeePrivilege> GetUserRole();
        //Roles? GetUserRole(string role);
    }
}
