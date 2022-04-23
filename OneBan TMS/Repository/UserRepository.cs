using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OneManDbContext _context;
        private readonly IPasswordHandler _passwordHandler;
        public UserRepository(OneManDbContext context, IPasswordHandler passwordHandler)
        {
            _context = context;
            _passwordHandler = passwordHandler;
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
 /*
        public async Task AddNewUser(EmployeeDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            //Todo: Zrobić walidację
            StringBuilder passwordConnector = new StringBuilder();
            passwordConnector.Append(ConvertByteArrayToString(passwordHash));
            passwordConnector.Append(ConvertByteArrayToString(passwordSalt));
            _context.Add(new Employee()
            {
                EmpLogin = user.EmpEmail,
                EmpName = user.EmpName,
                EmpSurname = user.EmpSurname,
                EmpEmail = user.EmpEmail,
                EmpPhoneNumber = user.EmpPhoneNumber,
                EmpCreatedAt = DateTime.Now,
                EmpPassword = passwordConnector.ToString()
            });
            await _context.SaveChangesAsync();
        }
        */
        public void GetPasswordParts(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password.Length != 260)
                throw new ArgumentException("Password is to short");
            string passwordBase64Hash = password.Substring(0, 88);
            string passwordBase64Salt = password.Substring(88, 172).Trim();
            passwordHash = _passwordHandler.ConvertStringToByteArray(passwordBase64Hash);
            passwordSalt = _passwordHandler.ConvertStringToByteArray(passwordBase64Salt);
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
        public async Task AddPrivilegesToUser(int employeeId, List<int> privileges)
        {
            foreach (int privilege in privileges)
            {
                await _context.EmployeePrivilegeEmployees.AddAsync(new EmployeePrivilegeEmployee()
                {
                    EpeIdEmployee = employeeId,
                    EpeIdEmployeePrivilage = privilege
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}