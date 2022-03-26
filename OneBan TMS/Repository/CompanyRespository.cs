using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class CompanyRespository : ICompanyRepository
    {
        private readonly OneManDbContext _context;
        private readonly IAddressRepository _addressRepository;
        public CompanyRespository(OneManDbContext context, IAddressRepository addressRepository)
        {
            _context = context;
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int idCompany)
        {
            return await _context
                .Companies
                .Where(x => x.CmpId == idCompany).FirstOrDefaultAsync();
        }

        public async Task AddNewCompany(CompanyDto newCompany)
        {
            Address address = GetAddress(newCompany);
            int? addressId = _addressRepository.GetAddressId(address);
            if (addressId is null)
            {
                addressId = _addressRepository.AddNewAddress(address);
            }
            
            _context.Companies.Add(new Company()
            {
                CmpName = newCompany.Name,
                CmpNip = newCompany.Nip,
                CmpNipPrefix = newCompany.NipPrefix,
                CmpRegon = newCompany.Regon,
                CmpKrsNumber = newCompany.KrsNumber,
                CmpLandline = newCompany.Landline,
                CmpIdAdress = (int)addressId
            });
            await _context.SaveChangesAsync();
        }

        private Address GetAddress(CompanyDto companyDto)
        {
            return new Address()
            {
                AdrTown = companyDto.Town,
                AdrStreet = companyDto.Street,
                AdrStreetNumber = companyDto.StreetNumber,
                AdrPostCode = companyDto.PostCode,
                AdrCountry = companyDto.Country
            };
        }


    }
}