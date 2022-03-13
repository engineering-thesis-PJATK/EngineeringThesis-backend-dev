using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicket Ticket { get; init; }

        public TicketController(ITicket ticket)
        {
            this.Ticket = ticket;
        }
        
        [HttpGet("GetTicketById")]
        public IActionResult GetTicketById(int ticketId)
        {
            if (ticketId < 1)
                return BadRequest();

            Ticket singleTicket = Ticket.GetTicketById(ticketId);
            if (singleTicket is null)
                return NotFound();
            
            return Ok(singleTicket);
        }
        
        [HttpGet("GetAllTickets")]
        public IActionResult GetAllTickets()
        {
            var ticketList = Ticket.GetAllTickets();
            if (ticketList.Any())
                return Ok(ticketList);
            else
                return NoContent();
        }
    }
}