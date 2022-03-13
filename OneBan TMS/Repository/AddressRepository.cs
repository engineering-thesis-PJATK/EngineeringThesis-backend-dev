using System.Collections.Generic;
using System.Linq;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Repository
{
    public class AddressRepository:IAddress
    {
        private OneManDbContext dbContext;
        public AddressRepository(OneManDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public Address GetAddressById(int addressId)
        {
            Address address = dbContext.Addresses.Where(address => address.AdrId == addressId)
                .Select(address => address).SingleOrDefault();
            return address;
        }

        public IList<Address> GetAllAddresses()
        {
            List<Address> addresses = dbContext.Addresses.ToList();
            return addresses;
        }
    }
}