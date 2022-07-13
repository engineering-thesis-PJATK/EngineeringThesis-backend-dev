using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Address;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesForCompany(int companyId);
        Task<Address> AddNewAddress(AddressDto newAddress, int companyId);
        Task UpdateAddress(AddressDto updatedAddress,int addressId);
        Task DeleteAddress(int addressId);
        Task<bool> IsAddressExists(int addressId);
    }
}