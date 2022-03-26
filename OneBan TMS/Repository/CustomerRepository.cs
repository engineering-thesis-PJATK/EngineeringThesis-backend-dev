using System.Collections.Generic;
using System.Linq;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OneManDbContext _context;
        public CustomerRepository(OneManDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.Where(x => x.CurId == customerId).SingleOrDefault();
        }

        public void AddNewCustomer()
        {
            throw new System.NotImplementedException();
        }
    }
    
}