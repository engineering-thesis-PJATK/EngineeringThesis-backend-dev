using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly OneManDbContext _context;
        private readonly IValidator<Company> _validator;
        public CompanyRepository(OneManDbContext context, IValidator<Company> validator)
        {
            _context = context;
            _validator = validator;
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
            Company company = new Company()
            {
                CmpName = newCompany.Name,
                CmpNip = newCompany.Nip,
                CmpNipPrefix = newCompany.NipPrefix,
                CmpRegon = newCompany.Regon,
                CmpKrsNumber = newCompany.KrsNumber,
                CmpLandline = newCompany.Landline
            };
            var validationResults = _validator.Validate(company);
            if (validationResults.IsValid)
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();   
            }
            else
            {
                throw new ArgumentException(validationResults.Errors[0].ErrorMessage);
            }
            
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

        public async Task<bool> ExistsCompany(int idCompany)
        {
            var result = await _context
                .Companies
                .Where(x =>
                    x.CmpId == idCompany)
                .AnyAsync();
            return result;
        }
    }
}