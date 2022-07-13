using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Filters.Customer;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerHandler _customerHandler;
        private readonly ICustomerFilter _customerFilter;
        public CustomerController(ICustomerRepository customerRepository, ICompanyRepository companyRepository, ICustomerHandler customerHandler, ICustomerFilter customerFilter)
        {
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _customerHandler = customerHandler;
            _customerFilter = customerFilter;
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
            if (!(await _companyRepository.IsCompanyExists(companyId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Company does not exist"));
            var tmp = (await _customerFilter.IsValid(newCustomer));
            if (!(tmp.Valid))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage(tmp.Message));
            }
            var customer = await _customerRepository
                .AddNewCustomer(newCustomer, companyId);
            return Ok(MessageProvider.GetSuccessfulMessage("Added successfully customer", null, customer.CurId));
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer, int customerId)
        {
            if (!(await _customerRepository.ExistsCustomer(customerId)))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage("Customer does not exist"));
            }
            var tmp = (await _customerFilter.IsValid(customer, customerId));
            if (!(tmp.Valid))
            {
                return BadRequest(MessageProvider.GetBadRequestMessage(tmp.Message));
            }
            await _customerRepository
                .UpdateCustomer(customer, customerId);
            return Ok(MessageProvider.GetSuccessfulMessage("Updated successfully customer"));
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

        [HttpDelete( "{customerId}")]

        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            if (!(await _customerRepository.ExistsCustomer(customerId)))
                return BadRequest(MessageProvider.GetBadRequestMessage("Customer does not exist"));
            await _customerRepository.DeleteCustomer(customerId);
            return Ok(MessageProvider.GetSuccessfulMessage("Customer deleted successfully"));
        }
    }
}