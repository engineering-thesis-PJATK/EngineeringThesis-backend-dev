using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Address;
using OneBan_TMS.Models.DTOs.Company;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        public CompanyController(OneManDbContext context, ICompanyRepository companyRepository, IAddressRepository addressRepository)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            IEnumerable<Company> companies = await _companyRepository.GetCompanies();
            if (companies is null)
                return NoContent();
            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            if (!(await _companyRepository.ExistsCompany(companyId)))
                return NotFound();
            Company company = await _companyRepository.GetCompanyById((int) companyId);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CompanyDto newCompany)
        {
            await _companyRepository.AddNewCompany(newCompany);
            return Ok("Added new company");
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDto updatedCompany, int companyId)
        {
            if (!(await _companyRepository.ExistsCompany(companyId)))
                return NoContent(); 
            await _companyRepository.UpdateCompany(updatedCompany, companyId);
            return Ok("Company updated");
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            if (!await _companyRepository.ExistsCompany(companyId))
                return NoContent();
            await _companyRepository.DeleteCompany(companyId);
            return Ok("Company deleted");
        }
        [HttpGet( "{companyId}/Addresses")]
        public async Task<IActionResult> GetAddressesForCompany(int companyId)
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAddressesForCompany(companyId);
            if (!(addresses.Any()))
                return NoContent();
            return Ok(addresses);
        }

        [HttpPost("{companyId}/Addresses")]
        public async Task<IActionResult> AddNewAddressForCompany(int companyId, [FromBody]AddressDto addressDto)
        {
            if (!(await _companyRepository.ExistsCompany(companyId)))
                return BadRequest($"There is no company with id {companyId}");
            await _addressRepository.AddNewAddress(addressDto, companyId);
            return Ok($"Added new address for company {companyId}");
        }

        [HttpPut("Addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressDto addressDto)
        {
            if (!(await _addressRepository.ExistsAddress(addressId)))
                return BadRequest($"There is no address with id {addressId}");
            await _addressRepository.UpdateAddress(addressDto, addressId);
            return Ok("Address updated");
        }

        [HttpDelete("Addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            if (!(await _addressRepository.ExistsAddress(addressId)))
                return BadRequest($"There is no address with id {addressId}");
            await _addressRepository.DeleteAddress(addressId);
            return Ok("Address deleted");
        }
    }
}