using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces
{
    public interface IPasswordHandler
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string ConvertByteArrayToString(byte[] array);
        byte[] ConvertStringToByteArray(string text);
    }
}
