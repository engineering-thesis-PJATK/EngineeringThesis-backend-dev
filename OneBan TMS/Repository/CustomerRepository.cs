using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        //Todo: Do poprawy !!!
        private readonly OneManDbContext _context;
        private readonly IValidator<CustomerDto> _customerDtoValidator;
        private readonly ICompanyRepository _companyRepository;
        public CustomerRepository(OneManDbContext context, IValidator<CustomerDto> customerDtoValidator, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
            _customerDtoValidator = customerDtoValidator;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context
                .Customers
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _context
                .Customers
                .Where(x => 
                    x.CurId == customerId)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsCustomer(int customerId)
        {
            var result = await _context
                .Customers
                .AnyAsync(x => x.CurId == customerId);
            return result;
        }

        public async Task AddNewCustomer(CustomerDto newCustomer, int companyId)
        {
            if (!(await _companyRepository.ExistsCompany(companyId)))
                throw new ArgumentException("Company not exists");
            _customerDtoValidator.ValidateAndThrow(newCustomer);
            Customer customer = new Customer()
            {
                CurName = newCustomer.CurName,
                CurSurname = newCustomer.CurSurname,
                CurEmail = newCustomer.CurEmail,
                CurPhoneNumber = newCustomer.CurPhoneNumber,
                CurPosition = newCustomer.CurPosition,
                CurComments = newCustomer.CurComments,
                CurCreatedAt = System.DateTime.Now,
                CurIdCompany = companyId
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(CustomerDto customer, int customerId)
        {
            _customerDtoValidator.ValidateAndThrow(customer);
            var customerToUpdate = await _context
                .Customers
                .SingleOrDefaultAsync();
            customerToUpdate.CurName = customer.CurName;
            customerToUpdate.CurSurname = customer.CurSurname;
            customerToUpdate.CurEmail = customer.CurEmail;
            customerToUpdate.CurPhoneNumber = customer.CurPhoneNumber;
            customerToUpdate.CurPosition = customer.CurPosition;
            customerToUpdate.CurComments = customer.CurComments;
            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerShortDto>> GetCustomersToSearch()
        {
            List<CustomerShortDto> customerShortDtos = new List<CustomerShortDto>();
            var customers = await _context
                .Customers
                .Select(x => new {x.CurId, x.CurEmail, x.CurName, x.CurSurname})
                .ToListAsync();
            foreach (var customer in customers)
            {
                    customerShortDtos.Add(new CustomerShortDto()
                    {
                        CurId = customer.CurId, 
                        CurEmail = customer.CurEmail, 
                        CurName = customer.CurName, 
                        CurSurname = customer.CurSurname
                    });
            }
            return customerShortDtos;
        }
    }
    
}