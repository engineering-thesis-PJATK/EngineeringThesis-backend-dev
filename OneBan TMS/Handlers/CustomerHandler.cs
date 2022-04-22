using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
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
    }
}