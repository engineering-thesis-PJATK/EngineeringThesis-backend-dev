using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly OneManDbContext _context;
        public TicketRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<List<TicketDto>> GetTickets()
        {
            var tickets = await _context
                                          .Tickets
                                          .ToListAsync();
            if (!(tickets.Any()))
            {
                return 
                    null;
            }
            return
                 tickets
                 .Select(ChangeTicketBaseToDto)
                 .ToList();
        }
        
        public async Task<TicketDto> GetTicketById(int ticketId)
        {
            Ticket ticket = await _context
                                  .Tickets
                                  .Where(x => x.TicId == ticketId)
                                  .SingleOrDefaultAsync();
            if (ticket is null)
            {
                return 
                    null;
            }

            return 
                ChangeTicketBaseToDto(ticket);
        }

        private TicketDto ChangeTicketBaseToDto(Ticket ticket)
        {
            return 
                new TicketDto()
                {   
                    Description = ticket.TicDescription, 
                    Id = ticket.TicId,
                    Name = ticket.TicName,
                    Topic = ticket.TicTopic,
                    CompletedAt = ticket.TicCompletedAt
                                        .GetValueOrDefault(),
                    CreatedAt = ticket.TicCreatedAt,
                    CustomerId = ticket.TicIdCustomer,
                    DueDate = ticket.TicDueDate,
                    EstimatedCost = ticket.TicEstimatedCost,
                    TicketPriorityId = ticket.TicIdTicketPriority,
                    TicketStatusId = ticket.TicIdTicketStatus,
                    TicketTypeId = ticket.TicIdTicketType
                };
        }

        private TicketPriorityDto ChangeTicketPriorityBaseToDto(TicketPriority ticketPriority)
        {
            return
                new TicketPriorityDto()
                {
                    Description = ticketPriority.TpiDescription,
                    Id = ticketPriority.TpiId,
                    Weight = ticketPriority.TpiWeight
                };
        }

        private TicketTypeDto ChangeTicketTypeBaseToDto(TicketType ticketType)
        {
            return
                new TicketTypeDto()
                {
                    Description = ticketType.TtpDescription,
                    Id = ticketType.TtpId,
                    Name = ticketType.TtpName
                };
        }

        private TicketStatusDto ChangeTicketStatusBaseToDto(TicketStatus ticketStatus)
        {
            return
                new TicketStatusDto()
                {
                    Description = ticketStatus.TstDescription,
                    Id = ticketStatus.TstId,
                    Name = ticketStatus.TstName
                };
        }
        
        public async Task<List<TicketTypeDto>> GetTicketTypes()
        {
            var ticketTypes = await _context
                                                  .TicketTypes
                                                  .ToListAsync();
            if (!(ticketTypes.Any()))
            {
                return
                    null;
            }
            return ticketTypes
                   .Select(ChangeTicketTypeBaseToDto)
                   .ToList();
        }

        public async Task<TicketTypeDto> GetTicketTypeById(int ticketTypeId)
        {
            var singleTicketType = await _context
                                         .TicketTypes
                                         .Where(ticketType => ticketType.TtpId == ticketTypeId)
                                         .SingleOrDefaultAsync();
            if (singleTicketType is null)
            {
                return 
                    null;
            }

            return 
                ChangeTicketTypeBaseToDto(singleTicketType);
        }

        public async Task<List<TicketPriorityDto>> GetTicketPriorities()
        {
            var ticketPriorities = await _context
                                                         .TicketPriorities
                                                         .ToListAsync();
            if (!(ticketPriorities.Any()))
            {
                return null;
            }
            
            return 
                ticketPriorities
                .Select(ChangeTicketPriorityBaseToDto)
                .ToList();
        }

        public async Task<TicketPriorityDto> GetTicketPriorityById(int ticketPriorityId)
        {
            var singleTicketPriority = await _context
                                             .TicketPriorities
                                             .Where(ticketPriority => ticketPriority.TpiId == ticketPriorityId)
                                             .SingleOrDefaultAsync();
            if (singleTicketPriority is null)
            {
                return 
                    null;
            }

            return 
                ChangeTicketPriorityBaseToDto(singleTicketPriority);
        }

        public async Task<TicketDto> UpdateTicket(int ticketId, TicketUpdateDto ticketUpdate)
        {
            var singleTicket = await _context
                                     .Tickets
                                     .Where(ticket => ticket.TicId.Equals(ticketId))
                                     .SingleOrDefaultAsync();
            if (singleTicket is not null)
            {
                singleTicket.TicName = ticketUpdate.Name;
                singleTicket.TicTopic = ticketUpdate.Topic;
                singleTicket.TicDescription = ticketUpdate.Description;
                singleTicket.TicEstimatedCost = ticketUpdate.EstimatedCost;
                singleTicket.TicDueDate = ticketUpdate.DueDate;
                singleTicket.TicCompletedAt = ticketUpdate.CompletedAt;
                singleTicket.TicIdTicketStatus = ticketUpdate.TicketStatusId;
                singleTicket.TicIdCustomer = ticketUpdate.CustomerId;
                singleTicket.TicIdTicketType = ticketUpdate.TicketTypeId;
                singleTicket.TicIdTicketPriority = ticketUpdate.TicketPriorityId;
                await _context.SaveChangesAsync();
                
                return GetTicketById(ticketId)
                       .Result;
            }

            return null;
        }

        public async Task<TicketDto> UpdateTicketStatusId(int ticketId, int ticketStatusId)
        {
            var singleTicket = await _context
                                     .Tickets
                                     .Where(ticket => ticket.TicId.Equals(ticketId))
                                     .SingleOrDefaultAsync();
            if (singleTicket is not null)
            {
                singleTicket.TicIdTicketStatus = ticketStatusId;
                await _context
                      .SaveChangesAsync();
                
                return 
                    GetTicketById(ticketId)
                    .Result;
            }

            return null;
        }

        public async Task<List<TicketStatusDto>> GetTicketStatuses()
        {
            var ticketStatuses = await _context
                .TicketStatuses
                .ToListAsync();
            if (!(ticketStatuses.Any()))
            {
                return 
                    null;
            }
            return
                ticketStatuses
                    .Select(ChangeTicketStatusBaseToDto)
                    .ToList();
        }

        public async Task<TicketStatusDto> GetTicketStatusById(int ticketStatusId)
        {
            var singleTicketStatus = await _context
                                           .TicketStatuses
                                           .Where(ticketStatus => ticketStatus.TstId == ticketStatusId)
                                           .SingleOrDefaultAsync();
            if (singleTicketStatus is null)
            {
                return 
                    null;
            }

            return 
                ChangeTicketStatusBaseToDto(singleTicketStatus);
        }
    }
}