using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly OneManDbContext _context;
        public CustomerHandler(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UniqueCustomerEmail(string customerEmail)
        {
            var result = await _context
                .Customers
                .AnyAsync(x => x.CurEmail == customerEmail);
            return !(result);
        }

        public async Task<bool> CheckEmailUnique(int customerId, string email)
        {
            var tmp = await _context
                .Customers
                .AnyAsync(x => x.CurId == customerId && x.CurEmail == email);
            return tmp;
        }

        public async Task<bool> CheckEmailUnique(string customerEmail)
        {
            var result = await _context
                .Customers
                .AnyAsync(x => x.CurEmail == customerEmail);
            return result;
        }

        public async Task<bool> CheckEmailUnique(string customerEmail, int customerId)
        {
            var result = await _context
                .Customers
                .AnyAsync(x => x.CurEmail == customerEmail
                    && x.CurId != customerId);
            return !(result);
        }
    }
}