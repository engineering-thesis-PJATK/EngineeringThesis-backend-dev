using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneBan_TMS.Repository
{
    public class UserTestRepository
    {
        /*List<User> _users = new List<User>();
        public void AddNewUser(UserDto user, byte[] PasswordHash, byte[] PasswordSalt)
        {
            _users.Add(new User() 
            { 
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                AccoutCreationTimeStamp = DateTime.Now,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            });
        }

        public User GetUserByEmail(string emailAddress)
        {
            return _users.Where(x => x.Email.Equals(emailAddress)).FirstOrDefault();
        }
        public Roles? GetUserRole(string role)
        {
            if (role == "admin")
                return Roles.Admin;
            if (role == "user")
                return Roles.User;
            return null;
        }
    }
    */
    }
}
