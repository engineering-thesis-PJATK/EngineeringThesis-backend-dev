using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly OneManDbContext _context;
        public AddressRepository(OneManDbContext context)
        {
            _context = context;
        }
        public int AddNewAddress(Address address)
        {
            var tmp = _context.Addresses.AddAsync(address);
            _context.SaveChanges();
            return address.AdrId;
        }

        public int? GetAddressId(Address address)
        {
            if (!(AddressExists(address)))
            {
               var addressId = _context
                    .Addresses
                    .Where(x => x.AdrTown.ToUpper() == address.AdrTown.ToUpper()
                                && x.AdrStreet.ToUpper() == address.AdrStreet.ToUpper()
                                && x.AdrStreetNumber.ToUpper() == address.AdrStreet.ToUpper()
                                && x.AdrPostCode.ToUpper() == address.AdrPostCode.ToUpper())
                    .Select(x => x.AdrId)
                    .SingleOrDefault();
                return addressId;
            }
            return null;
        }

        private bool AddressExists(Address address)
        {
            var result = _context
                .Addresses
                .Where(x => x.AdrTown.ToUpper() == address.AdrTown.ToUpper()
                            && x.AdrStreet.ToUpper() == address.AdrStreet.ToUpper()
                            && x.AdrStreetNumber.ToUpper() == address.AdrStreet.ToUpper()
                            && x.AdrPostCode.ToUpper() == address.AdrPostCode.ToUpper())
                .Select( x => x.AdrId)
                .Any();
            return result;
        }
    }
}