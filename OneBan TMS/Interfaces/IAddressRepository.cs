using System.Threading.Tasks;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface IAddressRepository
    {
        int AddNewAddress(Address address);
        int? GetAddressId(Address address);
    }
}