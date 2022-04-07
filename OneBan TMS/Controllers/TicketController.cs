using System;
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
        
        [HttpGet("{idTicket}")]
        public async Task<ActionResult<TicketDto>> GetTicketById(int idTicket)
        {
            if (idTicket < 1)
            {
                return BadRequest("Ticket id must be greater than 0");
            }
            TicketDto singleTicket = await _ticketRepository
                                        .GetTicketById(idTicket);
            if (singleTicket is null)
            {
                return NotFound($"No ticket with id: {idTicket} found");
            }
            
            return Ok(singleTicket);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetTickets()
        {
            var ticketList = await _ticketRepository
                                   .GetTickets();
            if (ticketList is null)
            {
                return BadRequest();
            }
            if (!(ticketList.Any()))
            {
                return NoContent();
            }
            
            return Ok(ticketList);
        }

        [HttpGet("Customer")]
        public async Task<ActionResult<List<CustomerShortDto>>> GetCustomersToSearch()
        {
            var customers = await  _customerRepository
                                                      .GetCustomersToSearch();
            if (customers is null)
            {
                return BadRequest();
            }
            if (!(customers.Any()))
            {
                return NoContent();
            }
            
            return Ok(customers);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<List<TicketTypeDto>>> GetTicketTypes()
        {
            var ticketTypes = await _ticketRepository
                                                     .GetTicketTypes();
            if (ticketTypes.Any())
            {
                return Ok(ticketTypes);
            }

            return NotFound("No ticket types found");
        }

        [HttpGet("Priorities")]
        public async Task<ActionResult<List<TicketPriorityDto>>> GetTicketPriorities()
        {
            var ticketPriorities = await _ticketRepository
                                                          .GetTicketPriorities();
            if (ticketPriorities.Any())
            {
                return Ok(ticketPriorities);
            }

            return NotFound("No ticket priorities found");
        }

        [HttpPut("{ticketId}")]
        public async Task<ActionResult<TicketDto>> UpdateTicketById(int ticketId, TicketUpdateDto ticketUpdate)
        {
            if (ModelState.IsValid)
            {
                if (ticketUpdate is null)
                {
                    return BadRequest("Ticket cannot be empty");
                }
                if (ticketId < 1)
                {
                    return BadRequest("Ticket id must be greater than 0");
                }

                var ticket = await _ticketRepository.UpdateTicket(ticketId, ticketUpdate);
                if (!(ticket is null))
                    return Ok(ticket);
            }

            return BadRequest("Operation was not executed");
        }
    }
}