using System.Collections.Generic;
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
        public IEnumerable<CustomerDto> GetAllCustomersDto()
        {
            return null;
        }
    }
    
}