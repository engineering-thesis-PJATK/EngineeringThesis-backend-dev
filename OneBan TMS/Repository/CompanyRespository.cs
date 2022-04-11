using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class CompanyRespository : ICompanyRepository
    {
        private readonly OneManDbContext _context;
        public CompanyRespository(OneManDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int idCompany)
        {
            return await _context
                .Companies
                .Where(x => x.CmpId == idCompany).FirstOrDefaultAsync();
        }

        public async Task AddNewCompany(CompanyDto newCompany)
        {
            _context.Companies.Add(new Company()
            {
                CmpName = newCompany.CmpName,
                CmpNip = newCompany.CmpNip,
                CmpNipPrefix = newCompany.CmpNipPrefix,
                CmpRegon = newCompany.CmpRegon,
                CmpKrsNumber = newCompany.CmpKrsNumber,
                CmpLandline = newCompany.CmpLandline
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(CompanyDto updatedCompanyDto, int idCompany)
        {
            var companyToUpdate = await _context
                .Companies
                .Where(x => x.CmpId == idCompany)
                .SingleOrDefaultAsync();
            companyToUpdate.CmpName = updatedCompanyDto.CmpName;
            companyToUpdate.CmpNip = updatedCompanyDto.CmpNip;
            companyToUpdate.CmpNipPrefix = updatedCompanyDto.CmpNipPrefix;
            companyToUpdate.CmpRegon = updatedCompanyDto.CmpRegon;
            companyToUpdate.CmpKrsNumber = updatedCompanyDto.CmpKrsNumber;
            companyToUpdate.CmpLandline = updatedCompanyDto.CmpLandline;
            await _context.SaveChangesAsync();
        }

        public bool ExistsCompany(int idCompany)
        {
            if(_context.Companies.Where(x => x.CmpId == idCompany).Any()) 
                return true;
            return false;
        }
    }
}