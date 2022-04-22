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
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
            await _customerRepository.AddNewCustomer(newCustomer, companyId);
            return Ok("Customer added");
        }
    }
}