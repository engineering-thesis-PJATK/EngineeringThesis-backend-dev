using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

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

        public async Task AddNewAddress(AddressDto newAddress, int idCompany)
        {
            if (!_companyRepository.ExistsCompany(idCompany))
                throw new ArgumentException("Company is not exists");
            _context.Addresses.Add(new Address()
            {
                AdrTown = newAddress.Town,
                AdrStreet = newAddress.Street,
                AdrStreetNumber = newAddress.StreetNumber,
                AdrPostCode = newAddress.PostCode,
                AdrCountry = newAddress.PostCode,
                AdrIdCompany = idCompany
            });
            await _context.SaveChangesAsync();
        }

        public Task UpdateAddress(AddressDto updatedAddress, int idAddress)
        {
            throw new System.NotImplementedException();
        }
    }
}