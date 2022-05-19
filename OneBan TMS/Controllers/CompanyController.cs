using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Address;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Messages;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IValidator<CompanyDto> _companyValidator;
        public CompanyController(OneManDbContext context, ICompanyRepository companyRepository, IAddressRepository addressRepository,  IValidator<CompanyDto> companyValidator)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _companyValidator = companyValidator;
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
                return NoContent();
            Company company = await _companyRepository.GetCompanyById((int) companyId);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CompanyDto newCompany)
        {
            var companyValidatorResult = await _companyValidator.ValidateAsync(newCompany);
            if (!(companyValidatorResult.IsValid))
            {
                return BadRequest(new MessageResponse()
                {
                    PropertyName = companyValidatorResult.Errors[0].PropertyName,
                    MessageContent = companyValidatorResult.Errors[0].ErrorMessage,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            await _companyRepository.AddNewCompany(newCompany);
            return Ok(new MessageResponse()
            {
                MessageContent = "Company added",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDto updatedCompany, int companyId)
        {
            if (!(await _companyRepository.ExistsCompany(companyId)))
            {
                return BadRequest(new MessageResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    MessageContent = "Company not exists"
                });
            }
            var companyValidationResult = await _companyValidator.ValidateAsync(updatedCompany);
            if (!(companyValidationResult.IsValid))
            {
                return BadRequest(new MessageResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    MessageContent = companyValidationResult.Errors[0].ErrorMessage,
                    PropertyName = companyValidationResult.Errors[0].PropertyName
                });
            }
            await _companyRepository.UpdateCompany(updatedCompany, companyId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Company updated",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            if (!await _companyRepository.ExistsCompany(companyId))
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Company not exists",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            await _companyRepository.DeleteCompany(companyId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Company successfully added",
                StatusCode = HttpStatusCode.OK
            });
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
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Company not exists",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            await _addressRepository.AddNewAddress(addressDto, companyId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Address successfully added",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPut("Addresses/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressDto addressDto)
        {
            if (!(await _addressRepository.ExistsAddress(addressId)))
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Address does not exist",
                    StatusCode = HttpStatusCode.BadRequest
                });
            await _addressRepository.UpdateAddress(addressDto, addressId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Address successfully updated",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpDelete("Addresses/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            if (!(await _addressRepository.ExistsAddress(addressId)))
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Address does not exist",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            await _addressRepository.DeleteAddress(addressId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Address successfully deleted",
                StatusCode = HttpStatusCode.OK
            });
        }
    }
}