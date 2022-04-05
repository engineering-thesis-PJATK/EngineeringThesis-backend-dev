using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
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
        private readonly IAddressRepository _addressRepository;
        public CompanyController(OneManDbContext context, ICompanyRepository companyRepository, IAddressRepository addressRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
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
            Company company = await _companyRepository.GetCompanyById((int) idCompany);
            if (company is null)
                return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CompanyDto newCompany)
        {
            await _companyRepository.AddNewCompany(newCompany);
            return Ok("Added new company");
        }

        [HttpPut("{idCompany}")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDto updatedCompany, int idCompany)
        {
            await _companyRepository.UpdateCompany(updatedCompany, idCompany);
            return Ok("Company updated");
        }

        [HttpGet( "{idCompany}/Address")]
        public async Task<IActionResult> GetAddressesForCompany(int idCompany)
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAddressesForCompany(idCompany);
            if (addresses is null)
                return NotFound();
            return Ok(addresses);
        }

        [HttpPost("{idCompany}/Address")]
        public IActionResult AddNewAddressForCompany(int idCompany)
        {
            return Ok($"Added new address for company {idCompany}");
        }
    }
}