using OneBan_TMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;

namespace OneBan_TMS.Handlers
{
    public class PasswordHandler : IPasswordHandler
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string ConvertByteArrayToString(byte[] array)
        {
            return Convert.ToBase64String(array);
        }

        public byte[] ConvertStringToByteArray(string text)
        {
            return Convert.FromBase64String(text);
        }
    }
}
