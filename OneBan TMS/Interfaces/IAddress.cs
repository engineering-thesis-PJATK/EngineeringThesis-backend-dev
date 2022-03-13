using System.Collections.Generic;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface IAddress
    {
        Address GetAddressById(int addressId);
        IList<Address> GetAllAddresses();
    }
}