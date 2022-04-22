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
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;

namespace OneBanTMS.IntegrationTests
{
    public class CustomerRepository_Test
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<CustomerDto> _validator;
        public CustomerRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
            _validator = new CustomerValidator(new CustomerHandler(_context));
        }

        [Test, Isolated]
        public async Task AddNewCustomer_PassValid_ShouldAddNewCustomerToDatabase()
        {
            var companyRepository = new CompanyRepository(_context, new CompanyValidator(new CompanyHandler(_context)));
            var customerRepository = new CustomerRepository(_context, _validator, companyRepository);
            
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
            await companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            await customerRepository.AddNewCustomer(newCustomerDto, companyId);
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
        [Test, Isolated]
        public async Task AddNewCustomer_WithNotValidEmail_ShouldThrowValidationError()
        {
            var companyRepository = new CompanyRepository(_context, new CompanyValidator(new CompanyHandler(_context)));
            var customerRepository = new CustomerRepository(_context, _validator, companyRepository);
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
            await companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            Func<Task> action = async () => await customerRepository.AddNewCustomer(newCustomerDto, companyId);
            await action.Should().ThrowExactlyAsync<ValidationException>();
        }
        [Test, Isolated]
        public async Task AddNewCustomer_WithNotValidCompanyId_ShouldThrowArgumentError()
        {
            var companyRepository = new CompanyRepository(_context, new CompanyValidator(new CompanyHandler(_context)));
            var customerRepository = new CustomerRepository(_context, _validator, companyRepository);
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
            await companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .MaxAsync(x => x.CmpId);
            companyId += 1;
            Func<Task> action = async () => await customerRepository.AddNewCustomer(newCustomerDto, companyId);
            await action.Should().ThrowExactlyAsync<ArgumentException>()
                .WithMessage("Company not exists");
        }
    }
}