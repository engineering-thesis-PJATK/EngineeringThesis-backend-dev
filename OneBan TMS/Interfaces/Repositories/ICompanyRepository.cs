using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Company;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompanyById(int idCompany);
        Task<Company> AddNewCompany(CompanyDto newCompany);
        Task UpdateCompany(CompanyDto updatedCompany, int idCompany);
        Task DeleteCompany(int companyId);
        Task<bool> ExistsCompany(int idCompany);
    }
}