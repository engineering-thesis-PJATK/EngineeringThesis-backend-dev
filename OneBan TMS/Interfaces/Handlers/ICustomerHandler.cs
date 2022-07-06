using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ICustomerHandler
    {
        Task<bool> CheckEmailUnique(string customerEmail);
        Task<bool> CheckEmailUnique(string customerEmail, int customerId);
    }
}