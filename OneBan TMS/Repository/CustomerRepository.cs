using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        //Todo: Do poprawy !!!
        private readonly OneManDbContext _context;
        public CustomerRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context
                .Customers
                .ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _context.Customers.Where(x => x.CurId == customerId).SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsCustomer(int customerId)
        {
            var result = await _context
                .Customers
                .AnyAsync(x => x.CurId == customerId);
            return result;
        }

        public Task AddNewCustomer(CustomerDto customerDto)
        {
            
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