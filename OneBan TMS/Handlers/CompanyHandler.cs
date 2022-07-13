using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<bool> IsCompanyNameUnique(string companyName)
        {
            var result =  await _context.Companies
                .Where(x => 
                    x.CmpName == companyName)
                .AnyAsync();
            return result;    
        }

        public async Task<bool> IsCompanyNameUnique(string companyName, int companyId)
        {
            var result = await _context.Companies
                .Where(x =>
                    x.CmpName == companyName
                    && x.CmpId != companyId)
                .AnyAsync();
            return result;
        }

        public async Task<string> GetNameOfCompanyById(int companyId)
        {
            var result = await _context.Companies
                .Where(x => x.CmpId == companyId)
                .Select(x => x.CmpName)
                .SingleOrDefaultAsync();
            return result;
        }
    }
}