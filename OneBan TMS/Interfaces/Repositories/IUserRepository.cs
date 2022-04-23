using OneBan_TMS.Enum;
using OneBan_TMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Employee> GetUserByEmail(string emailAddress);
        void GetPasswordParts(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<string> GetUserRole(string userEmail);
        Task AddPrivilegesToUser(int employeeId, List<int> privileges);
    }
}
