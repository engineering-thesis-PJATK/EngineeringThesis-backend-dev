using OneBan_TMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private byte[] ConvertStringToByteArray(string text)
        {
            return Convert.FromBase64String(text);
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

        public string GetMergedHashPassword(byte[] passwordHash, byte[] passwordSalt)
        {
            StringBuilder passwordBuilder = new StringBuilder();
            passwordBuilder.Append(ConvertByteArrayToString(passwordHash));
            passwordBuilder.Append(ConvertByteArrayToString(passwordSalt));
            return passwordBuilder.ToString();
        }
    }
}
