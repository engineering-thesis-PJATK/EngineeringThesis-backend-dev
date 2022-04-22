using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces
{
    public interface ICustomerHandler
    {
        Task<bool> UniqueCustomerEmail(string customerEmail);
    }
}