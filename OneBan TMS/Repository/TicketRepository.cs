using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models.DTOs.Kanban;
using OneBan_TMS.Models.DTOs.Ticket;

namespace OneBan_TMS.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly OneManDbContext _context;
        private readonly IStatusHandler _statusHandler;
        public TicketRepository(OneManDbContext context, IStatusHandler statusHandler)
        {
            _context = context;
            _statusHandler = statusHandler;
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
                                  .Where(ticket => ticket.TicId == ticketId)
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
                    TicDescription = ticket.TicDescription, 
                    TicId = ticket.TicId,
                    TicName = ticket.TicName,
                    TicTopic = ticket.TicTopic,
                    TicCompletedAt = ticket.TicCompletedAt
                                        .GetValueOrDefault(),
                    TicCreatedAt = ticket.TicCreatedAt,
                    TicCustomerId = ticket.TicIdCustomer,
                    TicDueDate = ticket.TicDueDate,
                    TicEstimatedCost = ticket.TicEstimatedCost,
                    TicTicketPriorityId = ticket.TicIdTicketPriority,
                    TicTicketStatusId = ticket.TicIdTicketStatus,
                    TicTicketTypeId = ticket.TicIdTicketType
                };
        }

        private CustomerDto ChangeCustomerBaseToDto(Customer customer)
        {
            return
                new CustomerDto()
                {
                    CurComments = customer.CurComments,
                    CurEmail = customer.CurEmail,
                    CurName = customer.CurName,
                    CurPosition = customer.CurPosition,
                    CurSurname = customer.CurSurname,
                    CurPhoneNumber = customer.CurPhoneNumber
                };
        }

        private CompanyDto changeCompanyBaseToDto(Company company)
        {
            return
                new CompanyDto()
                {
                    CmpLandline = company.CmpLandline,
                    CmpName = company.CmpName,
                    CmpNip = company.CmpNip,
                    CmpRegon = company.CmpRegon,
                    CmpKrsNumber = company.CmpKrsNumber,
                    CmpNipPrefix = company.CmpNipPrefix
                };
        }

        private TicketPriorityDto ChangeTicketPriorityBaseToDto(TicketPriority ticketPriority)
        {
            return
                new TicketPriorityDto()
                {
                    TpiDescription = ticketPriority.TpiDescription,
                    TpiId = ticketPriority.TpiId,
                    TpiWeight = ticketPriority.TpiWeight
                };
        }

        private TicketTypeDto ChangeTicketTypeBaseToDto(TicketType ticketType)
        {
            return
                new TicketTypeDto()
                {
                    TtpDescription = ticketType.TtpDescription,
                    TtpId = ticketType.TtpId,
                    TtpName = ticketType.TtpName
                };
        }

        private TicketStatusDto ChangeTicketStatusBaseToDto(TicketStatus ticketStatus)
        {
            return
                new TicketStatusDto()
                {
                    TstDescription = ticketStatus.TstDescription,
                    TstId = ticketStatus.TstId,
                    TstName = ticketStatus.TstName
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
                singleTicket.TicName = ticketUpdate.TicName;
                singleTicket.TicTopic = ticketUpdate.TicTopic;
                singleTicket.TicDescription = ticketUpdate.TicDescription;
                singleTicket.TicEstimatedCost = ticketUpdate.TicEstimatedCost;
                singleTicket.TicDueDate = ticketUpdate.TicDueDate;
                singleTicket.TicCompletedAt = ticketUpdate.TicCompletedAt;
                singleTicket.TicIdTicketStatus = ticketUpdate.TicTicketStatusId;
                singleTicket.TicIdCustomer = ticketUpdate.TicCustomerId;
                singleTicket.TicIdTicketType = ticketUpdate.TicTicketTypeId;
                singleTicket.TicIdTicketPriority = ticketUpdate.TicTicketPriorityId;
                await _context
                      .SaveChangesAsync();
                
                return 
                    GetTicketById(ticketId)
                    .Result;
            }

            return 
                null;
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

            return 
                null;
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

        public async Task DeleteTicketById(int ticketId)
        {
            Ticket ticket = await _context
                                  .Tickets
                                  .Where(ticket => ticket.TicId == ticketId)
                                  .SingleOrDefaultAsync();
            _context
            .Tickets
            .Remove(ticket);
            await _context
                .SaveChangesAsync();
        }

        public async Task<List<KanbanElement>> GetTicketsForEmployeeByStatus(int statusId, int employeeId)
        {
            List<KanbanElement> kanbanElements = new List<KanbanElement>();
            var ticketsList = await _context
                .EmployeeTickets
                .Where(x =>
                    x.EtsIdTicketNavigation.TicIdTicketStatus == statusId
                    && x.EtsIdEmployee == employeeId)
                .Select(x => new
                {
                    x.EtsIdTicketNavigation.TicId,
                    x.EtsIdTicketNavigation.TicName,
                    x.EtsIdTicketNavigation.TicTopic,
                    x.EtsIdTicketNavigation.TicDueDate
                }).ToListAsync();
            foreach (var ticket in ticketsList)
            {
                kanbanElements.Add(new KanbanElement()
                {
                    Id = ticket.TicId,
                    Name = ticket.TicName,
                    Topic = ticket.TicTopic,
                    DueDate = ticket.TicDueDate,
                    Type = (int)KanbanType.Ticket
                });
            }
            return kanbanElements;
        }

        public async Task UpdateTicketStatus(int ticketId, int statusId)
        {
            if (await _statusHandler.ExistsStatus(statusId))
                throw new ArgumentException("Status not exists");
            var ticket = await _context
                .Tickets
                .Where(x =>
                    x.TicId == ticketId)
                .SingleOrDefaultAsync();
            ticket.TicIdTicketStatus = statusId;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TicketCustomerCompanyDto>> GetTicketsForCustomTicketList()
        {
            var tickets = await _context
                .Tickets
                .ToListAsync();
            if (!(tickets.Any()))
            {
                return 
                    null;
            }

            List<TicketCustomerCompanyDto> ticketsForTicketList = new List<TicketCustomerCompanyDto>();
            foreach (var ticket in tickets)
            {
                Customer customer = await _context
                                          .Customers
                                          .Where(customer => customer.CurId == ticket.TicIdCustomer)
                                          .SingleOrDefaultAsync();
                Company company = await _context
                                        .Companies
                                        .Where(company => company.CmpId == customer.CurIdCompany)
                                        .SingleOrDefaultAsync();
                
                ticketsForTicketList.Add(new TicketCustomerCompanyDto()
                {
                    TicDescription = ticket.TicDescription, 
                    TicId = ticket.TicId,
                    TicName = ticket.TicName,
                    TicTopic = ticket.TicTopic,
                    TicCompletedAt = ticket.TicCompletedAt
                        .GetValueOrDefault(),
                    TicCreatedAt = ticket.TicCreatedAt,
                    TicCustomerId = ticket.TicIdCustomer,
                    TicDueDate = ticket.TicDueDate,
                    TicEstimatedCost = ticket.TicEstimatedCost,
                    TicTicketPriorityId = ticket.TicIdTicketPriority,
                    TicTicketStatusId = ticket.TicIdTicketStatus,
                    TicTicketTypeId = ticket.TicIdTicketType,
                    SingleCustomer = ChangeCustomerBaseToDto(customer),
                    SingleCompany = changeCompanyBaseToDto(company)
                });
            }
            return
                ticketsForTicketList;
        }
        
    }
}