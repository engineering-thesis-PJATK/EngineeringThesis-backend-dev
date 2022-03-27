using System;
using System.Linq;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class UserRepository : IUserRepostiory2
    {
        private readonly OneManDbContext _context;
        public UserRepository(OneManDbContext context)
        {
            _context = context;
        }

        public Employee GetUserByEmail(string emailAddress)
        {
            Employee employee = _context
                .Employees
                .Where(
                    x => x.EmpEmail.Equals(emailAddress))
                .SingleOrDefault();
            return employee;
        }

        public void AddNewUser(UserDto user, byte[] passwordHash, byte[] passwordSalt)
        {
            _context.Add(new Employee()
            {
                EmpLogin = user.Email,
                EmpName = user.Name,
                EmpSurname = user.LastName,
                EmpEmail = user.Email,
                EmpPhoneNumber = user.PhoneNumber,
                EmpCreatedAt = DateTime.Now
            });
        }

        public Roles? GetUserRole(string role)
        {
            throw new System.NotImplementedException();
        }
    }
}