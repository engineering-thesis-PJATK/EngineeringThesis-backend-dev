using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models.DTOs.Ticket;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators.TicketValidators;

namespace OneBanTMS.IntegrationTests
{
    public class TicketRepository_Test
    {
        private OneManDbContext _context;
        private CompanyRepository _companyRepository;
        private CustomerRepository _customerRepository;
        private TicketRepository _ticketRepository;
        private IStatusHandler _statusHandler;
        private ITicketNameHandler _ticketNameHandler;
        private IValidator<TicketNewDto> _newTicketValidator;
        [SetUp]
        public void Init()
        {
            _context = DbContextFactory.GetOneManDbContext();
            _companyRepository = new CompanyRepository(_context);
            //_customerRepository = new CustomerRepository(_context, _companyRepository);
            _statusHandler = new StatusHandler(_context);
            _ticketNameHandler = new TicketNameHandler(_context);
            _newTicketValidator = new TicketNewValidator();
            _ticketRepository = new TicketRepository(_context, _statusHandler, _ticketNameHandler, _newTicketValidator);
        }
        [Test, Isolated]
        public async Task TicketController_AddNewTicket_Verify_IfPresentInDataBase()
        {
            CustomerDto newCustomerDto = new CustomerDto()
            {
                CurName = "TestName",
                CurSurname = "TestSurname",
                CurEmail = "Test@TestEmail.com",
                CurComments = "TestComments",
                CurPosition = "TestPosition",
                CurPhoneNumber = "000000000"
            };
            CompanyDto newCompanyDto = new CompanyDto()
            {
                CmpName = "TestCompany",
                CmpNip = "0000000000",
                CmpNipPrefix = "PL"
            };
           
            await _companyRepository.AddNewCompany(newCompanyDto);
            var companyId = await _context
                .Companies
                .Where(x =>
                    x.CmpNip == newCompanyDto.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            await _customerRepository.AddNewCustomer(newCustomerDto, companyId);
            var customerId = await _context.Customers
                                           .Where(customer => customer.CurEmail.Equals(newCustomerDto.CurEmail)
                                                              && customer.CurName.Equals(newCustomerDto.CurName)
                                                              && customer.CurSurname.Equals(newCustomerDto.CurSurname))
                                           .Select(customer => customer.CurId).FirstOrDefaultAsync();
            var ticketPriorityId =
                await _context.TicketPriorities.Select(priority => priority.TpiId).FirstOrDefaultAsync();
            var ticketStatusId =
                await _context.TicketStatuses.Select(status => status.TstId).FirstOrDefaultAsync();
            var ticketTypeId =
                await _context.TicketTypes.Select(type => type.TtpId).FirstOrDefaultAsync();

            TicketNewDto newTicketDto = new TicketNewDto()
            {
                TicDescription = "TestDescription",
                TicTopic = "TestTopic",
                TicCompletedAt = null,
                TicDueDate = DateTime.UtcNow.AddDays(5),
                TicEstimatedCost = 2000.0M,
                TicIdCustomer = customerId,
                TicIdTicketPriority = ticketPriorityId,
                TicIdTicketStatus = ticketStatusId,
                TicIdTicketType = ticketTypeId
            };
            await _ticketRepository.AddTicket(newTicketDto);
            var countOfCreatedTickets = await _context
                .Tickets
                .Where(ticket =>
                    ticket.TicTopic.Equals(newTicketDto.TicTopic)
                    && ticket.TicDescription.Equals(newTicketDto.TicDescription)
                    && ticket.TicCompletedAt == null
                    && ticket.TicIdCustomer == newTicketDto.TicIdCustomer
                    && ticket.TicEstimatedCost == newTicketDto.TicEstimatedCost
                    && ticket.TicIdTicketPriority == newTicketDto.TicIdTicketPriority
                    && ticket.TicIdTicketStatus == newTicketDto.TicIdTicketStatus)
                .CountAsync();
            
            Assert.IsTrue(countOfCreatedTickets == 1);
        }
    }
}