using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface IOrganizationalTaskStatusHandler
    {
        Task<bool> StatusExists(int statusId);
        Task<int> GetStatusId(string statusName);
    }
}