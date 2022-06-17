using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models.DTOs.Messages;
using OneBan_TMS.Validators.CustomerValidators;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IValidator<CustomerDto> _customerValidator;
        public CustomerController(ICustomerRepository customerRepository, ICompanyRepository companyRepository, IValidator<CustomerDto> customerValidator)
        {
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _customerValidator = customerValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersList()
        {
            var customers = await _customerRepository
                .GetAllCustomers();
            if (customers is null)
                return NoContent();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int customerId)
        {
            if (!(await _customerRepository.ExistsCustomer(customerId)))
                return NoContent();
            var customer = await _customerRepository.GetCustomerById(customerId);
            return Ok(customer);
        }

        [HttpPost("{companyId}")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerDto newCustomer, int companyId)
        {
            if (await _companyRepository.ExistsCompany(companyId))
                return BadRequest(MessageHelper.GetBadRequestMessage("Company does not exist"));
            var validatorResults = await _customerValidator.ValidateAsync(newCustomer);
            if (!(validatorResults.IsValid))
            {
                return BadRequest(MessageHelper.GetBadRequestMessage(
                    validatorResults.Errors[0].ErrorMessage,
                    validatorResults.Errors[0].PropertyName
                 ));
            }
            
            var customer = await _customerRepository
                .AddNewCustomer(newCustomer, companyId);
            return Ok(MessageHelper.GetSuccessfulMessage("Added successfully customer", null, customer.CurId));
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer, int customerId)
        {
            if (!(await _customerRepository.ExistsCustomer(customerId)))
            {
                return BadRequest(MessageHelper.GetBadRequestMessage("Customer does not exist"));
            }
            var validationCustomerResult = await _customerValidator.ValidateAsync(customer);
            if (!(validationCustomerResult.IsValid))
            {
                return BadRequest(MessageHelper.GetBadRequestMessage(
                    validationCustomerResult.Errors[0].ErrorMessage,
                    validationCustomerResult.Errors[0].PropertyName)
                );
            }
            await _customerRepository
                .UpdateCustomer(customer, customerId);
            return Ok(MessageHelper.GetSuccessfulMessage("Updated successfully customer"));
        }

        [HttpGet("CompanyName")]
        public async Task<ActionResult<List<CustomerCompanyNameDto>>> GetCustomersWithCompanyName()
        {
            var customersList = await _customerRepository.GetCustomersWithCompanyName();
            if (!(customersList.Any()))
                return NoContent();
            return Ok(customersList);
        }
        [HttpGet("CompanyName/{customerId}")]
        public async Task<ActionResult<CustomerCompanyNameDto>> GetCustomerWithCompanyName(int customerId)
        {
            if(!(await _customerRepository.ExistsCustomer(customerId)))
                return NoContent();
            var customers = await _customerRepository.GetCustomerWithCompanyName(customerId);
            return Ok(customers);
        }
    }
}