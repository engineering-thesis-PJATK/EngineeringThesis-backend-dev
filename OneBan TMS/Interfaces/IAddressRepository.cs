using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesForCompany(int companyId);
        Task AddNewAddress(AddressDto newAddress, int companyId);
        Task UpdateAddress(AddressDto updatedAddress,int addressId);
        Task DeleteAddress(int addressId);
        Task<bool> ExistsAddress(int addressId);
    }
}