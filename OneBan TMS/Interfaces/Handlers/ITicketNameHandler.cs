using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ITicketNameHandler
    {
        Task<string> GetNewNameForTicket();
    }
}