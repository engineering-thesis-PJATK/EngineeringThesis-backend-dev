using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly OneManDbContext _context;
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(OneManDbContext context, ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            IEnumerable<Company> companies = await _companyRepository.GetCompanies();
            if (companies is null)
                return NotFound();
            return Ok(companies);
        }

        [HttpGet("{idCompany}")]
        public async Task<IActionResult> GetCompanyById(int? idCompany)
        {
            if (idCompany == null)
                return BadRequest();
            Company company = await _companyRepository.GetCompanyById((int)idCompany);
            if (company is null)
                return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyWithAddress([FromBody] CompanyDto newCompany)
        {
            await _companyRepository.AddNewCompany(newCompany);
            return Ok("Added new company");
        }
    }
}