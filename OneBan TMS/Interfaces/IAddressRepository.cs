using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesForCompany(int idCompany);
        Task AddNewAddress(AddressDto newAddress, int idCompany);
        Task UpdateAddress(AddressDto updatedAddress, int idAddress);
    }
}