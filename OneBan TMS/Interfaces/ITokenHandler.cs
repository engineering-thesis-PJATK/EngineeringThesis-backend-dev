using OneBan_TMS.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces
{
    public interface ITokenHandler
    {
        string CreateToken(string Email, Roles Role);
    }
}
