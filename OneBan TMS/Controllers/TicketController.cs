using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICustomerRepository _customerRepository;
        public TicketController(ITicketRepository ticketRepository, ICustomerRepository customerRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
        }
        
        //[HttpGet("{idTicket}"), Authorize (Roles = "Admin")]
        [HttpGet("{idTicket}")]
        public async Task<ActionResult> GetTicketById(int idTicket)
        {
            if (idTicket < 1)
                return BadRequest("Ticket Id must be greater than 0");

            Ticket singleTicket = await _ticketRepository.GetTicketById(idTicket);
            if (singleTicket is null)
                return NotFound($"There is no ticket with id {idTicket}");
            
            return Ok(singleTicket);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
            var ticketList = await _ticketRepository.GetTickets();
            if (ticketList is null)
                return BadRequest();
            if(!(ticketList.Any()))
                return NoContent();
            return Ok(ticketList);
        }

        [HttpGet("/customer")]
        public async Task<ActionResult<List<CustomerShortDto>>> GetCustomersToSearch()
        {
            var customers = await  _customerRepository.GetCustomersToSearch();
            if (customers is null)
                return BadRequest();
            if (!(customers.Any()))
                return NoContent();
            return Ok(customers);
        }
    }
}