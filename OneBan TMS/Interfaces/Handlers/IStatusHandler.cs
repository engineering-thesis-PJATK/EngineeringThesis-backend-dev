using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IStatusHandler
    {
        Task<bool> ExistsStatus(int statusId);
    }
}