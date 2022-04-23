using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;

namespace OneBan_TMS.Handlers
{
    public class CompanyHandler : ICompanyHandler
    {
        private readonly OneManDbContext _context;
        public CompanyHandler(OneManDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UniqueCompanyName(string companyName)
        {
            var result =  await _context
                .Companies
                .Where(x => x.CmpName == companyName)
                .AnyAsync();
            return result;    
        }
        
    }
}