using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OneManDbContext _context;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyHandler _companyHandler;

        public CustomerRepository(OneManDbContext context, ICompanyRepository companyRepository,
            ICompanyHandler companyHandler)
        {
            _context = context;
            _companyRepository = companyRepository;
            _companyHandler = companyHandler;
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
                .Include(x => x.Tickets)
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

        public async Task<Customer> AddNewCustomer(CustomerDto newCustomer, int companyId)
        {
            if (!(await _companyRepository.IsCompanyExists(companyId)))
                throw new ArgumentException("Company does not exist");
            Customer customer = newCustomer.GetCustomer();
            customer.CurIdCompany = companyId;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateCustomer(CustomerDto customer, int customerId)
        {
            var customerToUpdate = await _context
                .Customers
                .Where(x => x.CurId == customerId)
                .SingleOrDefaultAsync();
            if (customerToUpdate is null)
                throw new ArgumentException("Customer does not exists");
            /*
            customerToUpdate.CurName = customer.CurName;
            customerToUpdate.CurSurname = customer.CurSurname;
            customerToUpdate.CurEmail = customer.CurEmail;
            customerToUpdate.CurPhoneNumber = customer.CurPhoneNumber;
            customerToUpdate.CurPosition = customer.CurPosition;
            customerToUpdate.CurComments = customer.CurComments;
            */
            customerToUpdate = customer.GetUpdatedCustomer(customerToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerShortDto>> GetCustomersToSearch()
        {
            List<CustomerShortDto> customerShortDtos = new List<CustomerShortDto>();
            var customersList = await _context
                .Customers
                .ToListAsync();
            foreach (var customer in customersList)
            {

                /*
                    customerShortDtos.Add(new CustomerShortDto()
                    {
                        CurId = customer.CurId, 
                        CurEmail = customer.CurEmail, 
                        CurName = customer.CurName, 
                        CurSurname = customer.CurSurname
                    });
                    */
                customerShortDtos.Add(customer.GetCustomerShortDto());
            }

            return customerShortDtos;
        }

        public async Task<List<CustomerCompanyNameDto>> GetCustomersWithCompanyName()
        {
            List<CustomerCompanyNameDto> customerCompanyNameList = new List<CustomerCompanyNameDto>();
            var customerList = await GetAllCustomers();
            foreach (var customer in customerList)
            {
                //customerCompanyNameList.Add(await GetCustomerCompanyNameDtoFromCustomer(customer)); 
                string companyName = await _companyHandler.GetNameOfCompanyById(customer.CurIdCompany);
                customerCompanyNameList.Add(customer.GetCustomerCompanyNameDto(companyName));
            }

            return customerCompanyNameList;
        }

        public async Task<CustomerCompanyNameDto> GetCustomerWithCompanyName(int customerId)
        {
            var customer = await GetCustomerById(customerId);
            string companyName = await _companyHandler.GetNameOfCompanyById(customer.CurIdCompany);
            CustomerCompanyNameDto customerCompanyNameDto = customer.GetCustomerCompanyNameDto(companyName);
            //CustomerCompanyNameDto customerCompanyNameDto = await GetCustomerCompanyNameDtoFromCustomer(customer);
            return customerCompanyNameDto;
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _context.Customers
                .Where(x =>
                    x.CurId == customerId)
                .SingleOrDefaultAsync();
            if (customer is null)
                throw new ArgumentException("Customer does not exist");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        /*
            private async Task<CustomerCompanyNameDto> GetCustomerCompanyNameDtoFromCustomer(Customer customer)
            {
                return new CustomerCompanyNameDto()
                {
                    CurId = customer.CurId,
                    CurName = customer.CurName,
                    CurSurname = customer.CurSurname,
                    CurEmail = customer.CurEmail,
                    CurPhoneNumber = customer.CurPhoneNumber,
                    CurPosition = customer.CurPosition,
                    CurComments = customer.CurComments,
                    CurCreatedAt = customer.CurCreatedAt,
                    CurIdCompany = customer.CurIdCompany,
                    CurCompanyName = await _companyHandler.GetNameOfCompanyById(customer.CurIdCompany)
                };
            }
            */
    }
}