using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;

namespace OneBanTMS.IntegrationTests
{
    public class AddressRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<CompanyDto> _validator;
        //private readonly IValidator<>
        public AddressRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _validator = new CompanyValidator(new CompanyHandler(_context));
        }

        [Test, Isolated]
        public async Task AddAddress_PassValid_ShouldAddAddressForCompany()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var companyRepository = new CompanyRepository(_context, _validator);
            await companyRepository.AddNewCompany(companyDto);
            var companyId = await _context
                .Companies
                .Where(x => x.CmpNip == companyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            AddressDto addressDto = new AddressDto()
            {
                AdrTown = "Test",
                AdrStreet = "Test",
                AdrStreetNumber = "Test",
                AdrPostCode = "Test",
                AdrCountry = "Test"
            };
            var addressRepository = new AddressRepository(_context, companyRepository);
            await addressRepository.AddNewAddress(addressDto, companyId);
            var addressCount = await _context
                .Addresses
                .CountAsync(x =>
                    x.AdrTown == addressDto.AdrTown
                    && x.AdrStreet == addressDto.AdrStreet
                    && x.AdrStreetNumber == addressDto.AdrStreetNumber
                    && x.AdrPostCode == addressDto.AdrPostCode
                    && x.AdrCountry == addressDto.AdrCountry
                    && x.AdrIdCompany == companyId);
            Assert.That(addressCount, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task UpdateAddress_PassValid_ShouldUdpateCompanyInDatabase()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var companyRepository = new CompanyRepository(_context, _validator);
            await companyRepository.AddNewCompany(companyDto);
            var companyId = await _context
                .Companies
                .Where(x => x.CmpNip == companyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            AddressDto addressDto = new AddressDto()
            {
                AdrTown = "Test",
                AdrStreet = "Test",
                AdrStreetNumber = "Test",
                AdrPostCode = "Test",
                AdrCountry = "Test"
            };
            var addressRepository = new AddressRepository(_context, companyRepository);
            await addressRepository.AddNewAddress(addressDto, companyId);
            var addressId = await _context
                .Addresses
                .Where(x =>
                    x.AdrTown == addressDto.AdrTown
                    && x.AdrStreet == addressDto.AdrStreet
                    && x.AdrStreetNumber == addressDto.AdrStreetNumber
                    && x.AdrPostCode == addressDto.AdrPostCode
                    && x.AdrCountry == addressDto.AdrCountry)
                .Select(x =>
                    x.AdrId)
                .SingleOrDefaultAsync();
            AddressDto addressDtoToUpdate = new AddressDto()
            {
                AdrTown = "TestTest",
                AdrStreet = "TestTest",
                AdrStreetNumber = "TestTest",
                AdrPostCode = "TestTest",
                AdrCountry = "TestTest"
            };
            await addressRepository.UpdateAddress(addressDtoToUpdate, addressId);
            var addressCount = await _context
                .Addresses
                .CountAsync(x =>
                    x.AdrTown == addressDtoToUpdate.AdrTown
                    && x.AdrStreet == addressDtoToUpdate.AdrStreet
                    && x.AdrStreetNumber == addressDtoToUpdate.AdrStreetNumber
                    && x.AdrPostCode == addressDtoToUpdate.AdrPostCode
                    && x.AdrCountry == addressDtoToUpdate.AdrCountry);
            Assert.That(addressCount, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task DeleteAddress_ExistingId_ShouldDeleteFromDatabase()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "9999999999",
                CmpKrsNumber = "",
                CmpLandline = "",
                CmpRegon = "",
                CmpNipPrefix = "PL"
            };
            var companyRepository = new CompanyRepository(_context, _validator);
            await companyRepository.AddNewCompany(companyDto);
            var companyId = await _context
                .Companies
                .Where(x => x.CmpNip == companyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            AddressDto addressDto = new AddressDto()
            {
                AdrTown = "Test",
                AdrStreet = "Test",
                AdrStreetNumber = "Test",
                AdrPostCode = "Test",
                AdrCountry = "Test"
            };
            var addressRepository = new AddressRepository(_context, companyRepository);
            await addressRepository.AddNewAddress(addressDto, companyId);
            var addressId = await _context
                .Addresses
                .Where(x =>
                    x.AdrTown == addressDto.AdrTown
                    && x.AdrStreet == addressDto.AdrStreet
                    && x.AdrStreetNumber == addressDto.AdrStreetNumber
                    && x.AdrPostCode == addressDto.AdrPostCode
                    && x.AdrCountry == addressDto.AdrCountry)
                .Select(x =>
                    x.AdrId)
                .SingleOrDefaultAsync();
            await addressRepository.DeleteAddress(addressId);
            var countAddresses = await _context
                .Addresses
                .CountAsync(x =>
                    x.AdrTown == addressDto.AdrTown
                    && x.AdrStreet == addressDto.AdrStreet
                    && x.AdrStreetNumber == addressDto.AdrStreetNumber
                    && x.AdrPostCode == addressDto.AdrPostCode
                    && x.AdrCountry == addressDto.AdrCountry);
            Assert.That(countAddresses, Is.EqualTo(0));
        }
    }
}