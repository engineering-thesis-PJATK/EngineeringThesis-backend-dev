using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OneManDbContext _context;
        public UserRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> GetUserByEmail(string emailAddress)
        {
            Employee employee = await _context
                .Employees
                .Where(
                    x => x.EmpEmail.Equals(emailAddress))
                .SingleOrDefaultAsync();
            return employee;
        }
        public async Task AddNewUser(UserDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            StringBuilder passwordConnector = new StringBuilder();
            passwordConnector.Append(ConvertByteArrayToString(passwordHash));
            passwordConnector.Append(ConvertByteArrayToString(passwordSalt));
            _context.Add(new Employee()
            {
                EmpLogin = user.Email,
                EmpName = user.Name,
                EmpSurname = user.LastName,
                EmpEmail = user.Email,
                EmpPhoneNumber = user.PhoneNumber,
                EmpCreatedAt = DateTime.Now,
                EmpPassword = passwordConnector.ToString()
            });
            await _context.SaveChangesAsync();
        }
        public void GetPasswordParts(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password.Length != 260)
                throw new ArgumentException("Password is to short");
            string passwordBase64Hash = password.Substring(0, 88);
            string passwordBase64Salt = password.Substring(88, 172).Trim();
            passwordHash = ConvertStringToByteArray(passwordBase64Hash);
            passwordSalt = ConvertStringToByteArray(passwordBase64Salt);
            
        }
        public async Task<string> GetUserRole(string userEmail)
        {
            List<string> userRoles = await _context
                .EmployeePrivilegeEmployees
                .Where(x => x.EpeIdEmployeeNavigation.EmpEmail == userEmail)
                .Select(x => x.EpeIdEmployeePrivilageNavigation.EpvName)
                .ToListAsync();
            if (userRoles.Contains("Admin"))
                return "Admin";
            else
                return "User";
        }

        private string ConvertByteArrayToString(byte[] array)
        {
            return Convert.ToBase64String(array);
        }
        private byte[] ConvertStringToByteArray(string text)
        {
            return Convert.FromBase64String(text);
        }
    }
}