using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using OneBan_TMS.Helpers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models.DTOs.Ticket;
using OneBan_TMS.Models.DTOs.TimeEntry;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IValidator<TicketNewDto> _newTicketValidator;

        public TicketController(ITicketRepository ticketRepository, ICustomerRepository customerRepository,
            ITimeEntryRepository timeEntryRepository, IValidator<TicketNewDto> newTicketValidator)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
            _timeEntryRepository = timeEntryRepository;
            _newTicketValidator = newTicketValidator;
        }
        #region GetById
        [HttpGet("{idTicket}")]
        public async Task<ActionResult<TicketDto>> GetTicketById(int ticketId)
        {
            if (ticketId < 1)
            {
                return BadRequest("Ticket id must be greater than 0");
            }
            TicketDto singleTicket = await _ticketRepository
                .GetTicketById(ticketId);
            if (singleTicket is null)
            {
                return NotFound($"No ticket with id: {ticketId} found");
            }
            
            return Ok(singleTicket);
        }
        
        [HttpGet("Type/{typeId}")]
        public async Task<ActionResult<TicketTypeDto>> GetTicketTypeById(int typeId)
        {
            if (typeId < 1)
            {
                return 
                    BadRequest("Ticket type id must be greater than 0");
            }
            var singleTicketType = await _ticketRepository
                .GetTicketTypeById(typeId);
            if (singleTicketType is null)
            {
                return
                    NotFound($"No Ticket Priority was found for id {typeId}");
            }

            return
                Ok(singleTicketType);
            
        }
        
        [HttpGet("Priority/{priorityId}")]
        public async Task<ActionResult<TicketPriorityDto>> GetTicketPriorityById(int priorityId)
        {
            if (priorityId < 1)
            {
                return BadRequest("Ticket priority id must be greater than 0");
            }
            var singleTicketPriority = await _ticketRepository
                .GetTicketPriorityById(priorityId);
            if (singleTicketPriority is null)
            {
                return
                    NotFound($"No Ticket Priority was found for id {priorityId}");
            }

            return
                Ok(singleTicketPriority);
        }
        
        [HttpGet("Status/{statusId}")]
        public async Task<ActionResult<TicketPriorityDto>> GetTicketStatusById(int statusId)
        {
            if (statusId < 1)
            {
                return 
                    BadRequest("Ticket status id must be greater than 0");
            }
            var singleTicketStatus = await _ticketRepository
                .GetTicketStatusById(statusId);
            if (singleTicketStatus is null)
            {
                return
                    NotFound($"No Ticket Priority was found for id {statusId}");
            }

            return
                Ok(singleTicketStatus);
        }

        [HttpGet("TimeEntry/{timeEntryId}")]
        public async Task<ActionResult<TimeEntryGetDto>> GetTimeEntryById(int timeEntryId)
        {
            if (timeEntryId < 1)
            {
                return
                    BadRequest("Time entry id must be greater than 0");
            }

            var singleTimeEntry = await _timeEntryRepository
                .GetTimeEntryById(timeEntryId);
            if (singleTimeEntry is null)
            {
                return
                    NotFound($"No time entry was found for id {timeEntryId}");
            }

            return
                Ok(singleTimeEntry);
        }

        #endregion

        #region GetList
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
        [HttpGet("CustomTicket")]
        public async Task<ActionResult<List<TicketCustomerCompanyDto>>> GetCustomTickets()
        {
            var customTicketList = await _ticketRepository
                                    .GetTicketsForCustomTicketList();
            if (customTicketList is null)
            {
                return BadRequest();
            }
            if (!(customTicketList.Any()))
            {
                return NoContent();
            }
            
            return Ok(customTicketList.OrderBy(ticket => ticket.TicId));
        }

        [HttpGet("Customer")]
        public async Task<ActionResult<List<CustomerShortDto>>> GetCustomersToSearch()
        {
            var customers = await  _customerRepository
                                                      .GetCustomersToSearch();
            if (customers is null)
            {
                return 
                    BadRequest();
            }
            if (!(customers.Any()))
            {
                return 
                    NoContent();
            }
            
            return 
                Ok(customers);
        }

        [HttpGet("Type")]
        public async Task<ActionResult<List<TicketTypeDto>>> GetTicketTypes()
        {
            var ticketTypes = await _ticketRepository
                                                     .GetTicketTypes();
            if (ticketTypes.Any())
            {
                return 
                    Ok(ticketTypes);
            }

            return 
                NotFound("No ticket types found");
        }

        [HttpGet("Priority")]
        public async Task<ActionResult<List<TicketPriorityDto>>> GetTicketPriorities()
        {
            var ticketPriorities = await _ticketRepository
                                                          .GetTicketPriorities();
            if (ticketPriorities.Any())
            {
                return 
                    Ok(ticketPriorities);
            }

            return 
                NotFound("No ticket priorities found");
        }

        [HttpGet("Status")]
        public async Task<ActionResult<List<TicketPriorityDto>>> GetTicketStatuses()
        {
            var ticketStatuses = await _ticketRepository
                .GetTicketStatuses();
            if (ticketStatuses.Any())
            {
                return 
                    Ok(ticketStatuses);
            }

            return 
                NotFound("No ticket statuses found");
        }
        
        [HttpGet("TimeEntry")]
        public async Task<ActionResult<List<TimeEntryGetDto>>> GetTimeEntries()
        {
            var timeEntries = await _timeEntryRepository
                                                        .GetTimeEntries();
            if (timeEntries is null)
            {
                return 
                    BadRequest();
            }
            if (!(timeEntries.Any()))
            {
                return 
                    NoContent();
            }
            
            return Ok(timeEntries);
        }
        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> AddNewTicket(TicketNewDto ticketNewDto)
        {
            var validatorResult = await _newTicketValidator.ValidateAsync(ticketNewDto);
            if (!(validatorResult.IsValid))
                return BadRequest(MessageHelper.GetBadRequestMessage(
                    validatorResult.Errors[0].ErrorMessage,
                    validatorResult.Errors[0].PropertyName));
            var ticket = await _ticketRepository.AddTicket(ticketNewDto);
            return Ok(MessageHelper.GetSuccessfulMessage("Ticket added successfully", null, ticket.TicId));
        }
        #endregion

        #region Put
        [HttpPut("{ticketId}")]
        public async Task<ActionResult<TicketDto>> UpdateTicketById(int ticketId, TicketUpdateDto ticketUpdate)
        {
            if (ModelState.IsValid)
            {
                if (ticketUpdate is null)
                {
                    return 
                        BadRequest("Ticket cannot be empty");
                }
                if (ticketId < 1)
                {
                    return 
                        BadRequest("Ticket id must be greater than 0");
                }

                var singleTicket = await _ticketRepository.UpdateTicket(ticketId, ticketUpdate);
                if (!(singleTicket is null))
                {
                    return
                        Ok(singleTicket);
                }
            }

            return 
                BadRequest("Operation was not executed");
        }
        
        [HttpPut("{timeEntryId}")]
        public async Task<ActionResult<TicketDto>> UpdateTimeEntryById(int timeEntryId, TimeEntryUpdateDto timeEntryUpdate)
        {
            if (ModelState.IsValid)
            {
                if (timeEntryUpdate is null)
                {
                    return 
                        BadRequest("Time entry cannot be empty");
                }
                if (timeEntryId < 1)
                {
                    return 
                        BadRequest("Time entry id must be greater than 0");
                }

                var singleTimeEntry = await _timeEntryRepository.UpdateTimeEntry(timeEntryId, timeEntryUpdate);
                if (!(singleTimeEntry is null))
                {
                    return
                        Ok(singleTimeEntry);
                }
            }

            return 
                BadRequest("Operation was not executed");
        }
        #endregion
        
        #region Patch
        [HttpPatch("{ticketId}")]
        public async Task<ActionResult<TicketDto>> UpdateTicketStatusId(int ticketId, TicketStatusIdPatchDto newStatus)
        {
            if (ticketId < 1)
            {
                return 
                    BadRequest("Ticket id must be greater than 0");
            }
            if (newStatus is null)
            {
                return
                    BadRequest("Ticket status was not provided");
            }
            switch (newStatus.TstId)    
            {
                case < 1:
                    return 
                        BadRequest("Ticket status must be greater than 0");
                case > 5:
                    return 
                        BadRequest("Ticket status must not be greater than 5");
            }

            var singleTicket = await _ticketRepository
                                     .UpdateTicketStatusId(ticketId, newStatus.TstId);
            if (!(singleTicket is null))
            {
                return
                    Ok(singleTicket);
            }
            return 
                NotFound("No ticket found to update");
        }
        #endregion
        
        #region Delete
        [HttpDelete("Ticket/{ticketId:int}")]
        public async Task<ActionResult> DeleteTicketById(int ticketId)
        {
            if (ticketId < 1)
            {
                return 
                    BadRequest("Ticket id must be greater than 0");
            }

            await _ticketRepository
                .DeleteTicketById(ticketId);
            return
                Ok($"Ticket with id {ticketId} has been deleted");
        }
        
        [HttpDelete("TimeEntry/{timeEntryId:int}")]
        public async Task<ActionResult> DeleteTimeEntryById(int timeEntryId)
        {
            if (timeEntryId < 1)
            {
                return 
                    BadRequest("Time entry id must be greater than 0");
            }

            await _timeEntryRepository
                .DeleteTimeEntryById(timeEntryId);
            return
                Ok($"Time entry with id {timeEntryId} has been deleted");
        }
        #endregion
    }
}