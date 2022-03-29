using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompanyById(int idCompany);
        Task AddNewCompany(CompanyDto newCompany);
        Task UpdateCompany(CompanyDto updatedCompany, int idCompany);
        bool ExistsCompany(int idCompany);
    }
}