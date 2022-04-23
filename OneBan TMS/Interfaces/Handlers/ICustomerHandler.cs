using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        Task<bool> UniqueCustomerEmail(string customerEmail);
    }
}