using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;
        public CustomerController(ICustomerRepository customerRepository, ICompanyRepository companyRepository)
        {
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
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
                return BadRequest($"Company with id: {companyId} is not exists");
            await _customerRepository
                .AddNewCustomer(newCustomer, companyId);
            return Ok("Customer added");
            //Todo: Przegadać kwestie miejsca employees
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer, int customerId)
        {
            if (await _customerRepository.ExistsCustomer(customerId))
                return BadRequest("Customer not exists");
            await _customerRepository
                .UpdateCustomer(customer, customerId);
            return Ok("Customer updated");
        }
    }
}