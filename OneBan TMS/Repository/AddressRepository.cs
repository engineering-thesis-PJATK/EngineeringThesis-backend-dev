using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Address;

namespace OneBan_TMS.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly OneManDbContext _context;
        private readonly ICompanyRepository _companyRepository;
        public AddressRepository(OneManDbContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }
        public async Task<IEnumerable<Address>> GetAddressesForCompany(int idCompany)
        {
            return await _context
                .Addresses
                .Where(x => 
                    x.AdrIdCompany == idCompany)
                .ToListAsync();
        }
        public async Task<Address> AddNewAddress(AddressDto newAddress, int idCompany)
        {
            Address address = newAddress.GetAddress();
            address.AdrIdCompany = idCompany;
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        public async Task UpdateAddress(AddressDto updatedAddress, int addressId)
        {
            var address = await _context
                .Addresses
                .Where(x => x.AdrId == addressId)
                .SingleOrDefaultAsync();
            if (address is null)
                throw new ArgumentException("Address not exists");
            address = updatedAddress.GetAddressToUpdate(address);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAddress(int addressId)
        {
            var address = await _context
                    .Addresses
                    .Where(x => x.AdrId == addressId)
                    .SingleOrDefaultAsync();
            if (address is null)
                throw new ArgumentException("Address does not exist");
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsAddressExists(int addressId)
        {
            return await _context
                .Addresses
                .Where(x => x.AdrId == addressId)
                .AnyAsync();
        }

    }
}