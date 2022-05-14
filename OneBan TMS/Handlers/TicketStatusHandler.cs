using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;

namespace OneBan_TMS.Handlers
{
    public class TicketStatusHandler : ITicketStatusHandler
    {
        public Task<bool> TicketStatusExists(string statusName)
        {
            throw new System.NotImplementedException();
        }
    }
}