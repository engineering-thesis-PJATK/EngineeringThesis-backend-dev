using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;

namespace OneBanTMS.IntegrationTests
{
    public class CustomerRepository_Test
    {
        private OneManDbContext _context;
        private CompanyRepository _companyRepository;
        private CustomerRepository _customerRepository;
        public CustomerRepository_Test()
        {
            
        }
        [SetUp]
        public void init()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _companyRepository = new CompanyRepository(_context);
            _customerRepository = new CustomerRepository(_context, _companyRepository);
        }
        [Test, Isolated]
        public async Task AddNewCustomer_PassValid_ShouldAddNewCustomerToDatabase()
        {
            CustomerDto newCustomerDto = new CustomerDto()
            {
                CurName = "TestName",
                CurSurname = "TestSurname",
                CurEmail = "Test@TestEmail.com",
                CurComments = "TestComments",
                CurPosition = "TestPosition",
                CurPhoneNumber = "000000000"
            };
            CompanyDto newCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "0000000000",
                CmpNipPrefix = "PL"
            };
            await _companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            await _customerRepository.AddNewCustomer(newCustomerDto, companyId);
            var countOfCustomers = await _context
                .Customers
                .Where(x =>
                    x.CurName == newCustomerDto.CurName
                    && x.CurSurname == newCustomerDto.CurSurname
                    && x.CurEmail == newCustomerDto.CurEmail
                    && x.CurComments == newCustomerDto.CurComments
                    && x.CurPosition == newCustomerDto.CurPosition
                    && x.CurPhoneNumber == newCustomerDto.CurPhoneNumber)
                .CountAsync();
            Assert.That(countOfCustomers, Is.EqualTo(1));
        }
/*
        [Test, Isolated]
        public async Task AddNewCustomer_WithNotValidEmail_ShouldThrowValidationError()
        {
            CustomerDto newCustomerDto = new CustomerDto()
            {
                CurName = "TestName",
                CurSurname = "TestSurname",
                CurEmail = "Test",
                CurComments = "TestComments",
                CurPosition = "TestPosition",
                CurPhoneNumber = "000000000"
            };
            CompanyDto newCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "0000000000",
                CmpNipPrefix = "PL"
            };
            await _companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            Func<Task> action = async () => await _customerRepository.AddNewCustomer(newCustomerDto, companyId);
            await action.Should().ThrowExactlyAsync<ValidationException>();
        }
*/
        [Test, Isolated]
        public async Task AddNewCustomer_WithNotValidCompanyId_ShouldThrowArgumentError()
        {
            CustomerDto newCustomerDto = new CustomerDto()
            {
                CurName = "TestName",
                CurSurname = "TestSurname",
                CurEmail = "Test@Test.com",
                CurComments = "TestComments",
                CurPosition = "TestPosition",
                CurPhoneNumber = "000000000"
            };
            CompanyDto newCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "0000000000",
                CmpNipPrefix = "PL"
            };
            await _companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .MaxAsync(x => x.CmpId);
            companyId += 1;
            Func<Task> action = async () => await _customerRepository.AddNewCustomer(newCustomerDto, companyId);
            await action.Should().ThrowExactlyAsync<ArgumentException>()
                .WithMessage("Company does not exist");
        }

        [Test, Isolated]
        public async Task UpdateCustomer_PassValid_ShouldUpdateCustomerInDatabase()
        {
            CustomerDto newCustomerDto = new CustomerDto()
            {
                CurName = "TestName",
                CurSurname = "TestSurname",
                CurEmail = "Test@TestEmail.com",
                CurComments = "TestComments",
                CurPosition = "TestPosition",
                CurPhoneNumber = "000000000"
            };
            CompanyDto newCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "0000000000",
                CmpNipPrefix = "PL"
            };
            await _companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            await _customerRepository.AddNewCustomer(newCustomerDto, companyId);
            var customerId = await _context
                .Customers
                .Where(x =>
                    x.CurEmail == newCustomerDto.CurEmail)
                .Select(x => x.CurId)
                .SingleOrDefaultAsync();
            
            CustomerDto customer = new CustomerDto()
            {
                CurName = "TestTestName",
                CurSurname = "TestTestSurname",
                CurEmail = "TestTest@TestEmail.com",
                CurComments = "TestTestComments",
                CurPosition = "TestTestPosition",
                CurPhoneNumber = "1111111111"
            };
            await _customerRepository.UpdateCustomer(customer, customerId);
            
            //Todo: Brak asercji
        }
    }
}