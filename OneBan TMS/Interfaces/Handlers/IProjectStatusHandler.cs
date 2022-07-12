using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IProjectStatusHandler
    {
        Task<bool> IsProjectStatusExist(int projectStatusId);
    }
}