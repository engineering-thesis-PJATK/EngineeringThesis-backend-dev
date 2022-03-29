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
                CmpName = newCompany.Name,
                CmpNip = newCompany.Nip,
                CmpNipPrefix = newCompany.NipPrefix,
                CmpRegon = newCompany.Regon,
                CmpKrsNumber = newCompany.KrsNumber,
                CmpLandline = newCompany.Landline
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(CompanyDto updatedCompanyDto, int idCompany)
        {
            var companyToUpdate = await _context
                .Companies
                .Where(x => x.CmpId == idCompany)
                .SingleOrDefaultAsync();
            companyToUpdate.CmpName = updatedCompanyDto.Name;
            companyToUpdate.CmpNip = updatedCompanyDto.Nip;
            companyToUpdate.CmpNipPrefix = updatedCompanyDto.NipPrefix;
            companyToUpdate.CmpRegon = updatedCompanyDto.Regon;
            companyToUpdate.CmpKrsNumber = updatedCompanyDto.KrsNumber;
            companyToUpdate.CmpLandline = updatedCompanyDto.Landline;
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