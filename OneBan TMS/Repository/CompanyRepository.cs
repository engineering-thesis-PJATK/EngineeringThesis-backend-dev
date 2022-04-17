using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<CompanyDto> _validator;
        public CompanyRepository(OneManDbContext context, IValidator<CompanyDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyById(int companyId)
        {
            var company = await _context
                .Companies
                .Where(x => x.CmpId == companyId)
                .FirstOrDefaultAsync();
            return company;
        }

        public async Task AddNewCompany(CompanyDto newCompany)
        {
            _validator.ValidateAndThrow(newCompany);
            Company company = new Company()
            {
                CmpName = newCompany.CmpName,
                CmpNip = newCompany.CmpNip,
                CmpNipPrefix = newCompany.CmpNipPrefix,
                CmpRegon = newCompany.CmpRegon,
                CmpKrsNumber = newCompany.CmpKrsNumber,
                CmpLandline = newCompany.CmpLandline
            };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(CompanyDto updatedCompanyDto, int companyId)
        {
            _validator.ValidateAndThrow(updatedCompanyDto);
            var companyToUpdate = await _context
                    .Companies
                    .Where(x => x.CmpId == companyId)
                    .SingleOrDefaultAsync();
            if (companyToUpdate is null)
                throw new ArgumentException("Company not exists");
            companyToUpdate.CmpName = updatedCompanyDto.CmpName;
            companyToUpdate.CmpNip = updatedCompanyDto.CmpNip;
            companyToUpdate.CmpNipPrefix = updatedCompanyDto.CmpNipPrefix;
            companyToUpdate.CmpRegon = updatedCompanyDto.CmpRegon;
            companyToUpdate.CmpKrsNumber = updatedCompanyDto.CmpKrsNumber;
            companyToUpdate.CmpLandline = updatedCompanyDto.CmpLandline;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int companyId)
        {
            var company = await _context
                .Companies
                .Where(x =>
                    x.CmpId == companyId)
                .SingleOrDefaultAsync();
            if (company is null)
                throw new ArgumentException("Company not exists");
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsCompany(int companyId)
        {
            var result = await _context
                .Companies
                .Where(x =>
                    x.CmpId == companyId)
                .AnyAsync();
            return result;
        }
    }
}