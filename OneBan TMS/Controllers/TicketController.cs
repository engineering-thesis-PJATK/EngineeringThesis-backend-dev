using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
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
    }
}