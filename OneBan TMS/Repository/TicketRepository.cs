using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return tickets.Select(ticket => new TicketDto()
            {
                Description = ticket.TicDescription, 
                Id = ticket.TicId,
                Name = ticket.TicName,
                Topic = ticket.TicTopic,
                CompletedAt = ticket.TicCompletedAt.GetValueOrDefault(),
                CreatedAt = ticket.TicCreatedAt,
                CustomerId = ticket.TicIdCustomer,
                DueDate = ticket.TicDueDate,
                EstimatedCost = ticket.TicEstimatedCost,
                TicketPriorityId = ticket.TicIdTicketPriority,
                TicketStatusId = ticket.TicIdTicketStatus,
                TicketTypeId = ticket.TicIdTicketType
            }).ToList();
        }
        
        public async Task<TicketDto> GetTicketById(int ticketId)
        {
            Ticket ticket = await _context
                                    .Tickets
                                    .Where(x => x.TicId == ticketId)
                                    .SingleOrDefaultAsync();
         
            return new TicketDto()
                {   
                    Description = ticket.TicDescription, 
                    Id = ticket.TicId,
                    Name = ticket.TicName,
                    Topic = ticket.TicTopic,
                    CompletedAt = ticket.TicCompletedAt.GetValueOrDefault(),
                    CreatedAt = ticket.TicCreatedAt,
                    CustomerId = ticket.TicIdCustomer,
                    DueDate = ticket.TicDueDate,
                    EstimatedCost = ticket.TicEstimatedCost,
                    TicketPriorityId = ticket.TicIdTicketPriority,
                    TicketStatusId = ticket.TicIdTicketStatus,
                    TicketTypeId = ticket.TicIdTicketType
                };
        }
        
        public async Task<List<TicketTypeDto>> GetTicketTypes()
        {
            var ticketTypes = await _context
                                                  .TicketTypes
                                                  .ToListAsync();
            return ticketTypes.Select(ticketType => new TicketTypeDto()
            {
                Description = ticketType.TtpDescription, 
                Id = ticketType.TtpId, 
                Name = ticketType.TtpName
            }).ToList();
        }

        public async Task<List<TicketPriorityDto>> GetTicketPriorities()
        {
            var ticketPriorities = await _context
                                                         .TicketPriorities
                                                         .ToListAsync();
            return ticketPriorities.Select(ticketPriority => new TicketPriorityDto()
            {
                Description = ticketPriority.TpiDescription, 
                Id = ticketPriority.TpiId,
                Weight = ticketPriority.TpiWeight
            }).ToList();
        }

        public async Task<TicketDto> UpdateTicket(int ticketId, TicketUpdateDto ticketUpdate)
        {
            var singleTicket = await _context
                                     .Tickets
                                     .Where(ticket => ticket.TicId.Equals(ticketId))
                                     .SingleOrDefaultAsync();
            if (!(singleTicket is null))
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
                return GetTicketById(ticketId).Result;
            }

            return null;
        }
    }
}