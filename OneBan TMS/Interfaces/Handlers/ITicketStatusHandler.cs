using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ITicketStatusHandler
    {
        Task<bool> TicketStatusExists(string statusName);
    }
}