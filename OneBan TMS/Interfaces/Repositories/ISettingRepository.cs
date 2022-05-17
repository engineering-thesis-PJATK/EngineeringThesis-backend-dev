using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Setting;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        Task<IEnumerable<UserPrivileges>> GetUserWithPrivileges();
    }
}