using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Company does not exist",
                    StatusCode = HttpStatusCode.BadRequest
                });
            var validatorResults = await _customerValidator.ValidateAsync(newCustomer);
            if (!(validatorResults.IsValid))
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = validatorResults.Errors[0].ErrorMessage,
                    PropertyName = validatorResults.Errors[0].PropertyName,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            
            var customer = await _customerRepository
                .AddNewCustomer(newCustomer, companyId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Added successfully customer",
                StatusCode = HttpStatusCode.OK,
                ObjectId = customer.CurId 
            });
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer, int customerId)
        {
            if (await _customerRepository.ExistsCustomer(customerId))
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = "Customer does not exist",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            var validationCustomerResult = await _customerValidator.ValidateAsync(customer);
            if (validationCustomerResult.IsValid)
            {
                return BadRequest(new MessageResponse()
                {
                    MessageContent = validationCustomerResult.Errors[0].ErrorMessage,
                    PropertyName = validationCustomerResult.Errors[0].PropertyName,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            await _customerRepository
                .UpdateCustomer(customer, customerId);
            return Ok(new MessageResponse()
            {
                MessageContent = "Updated successfully customer",
                StatusCode = HttpStatusCode.OK
            });
        }
    }
}