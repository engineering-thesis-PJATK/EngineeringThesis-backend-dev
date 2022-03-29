using System;
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