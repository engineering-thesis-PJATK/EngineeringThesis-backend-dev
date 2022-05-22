using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Graph;
using NUnit.Framework;
using OneBan_TMS.Enum;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models.DTOs.Setting;
using OneBan_TMS.Models.DTOs.Ticket;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators;
using OneBan_TMS.Validators.TicketValidators;

namespace OneBanTMS.IntegrationTests
{
    public class KanbanRepository_Test
    {
        private OneManDbContext _context;
        private TicketNewValidator _ticketNewValidator;
        private StatusHandler _statusHandler;
        
        [SetUp]
        public void Init()
        {
            _context = DbContextFactory.GetOneManDbContext();
            _ticketNewValidator = new TicketNewValidator();
            _statusHandler = new StatusHandler(_context);
        }
        [Test, Isolated]
        public async Task UpdateKanbanElementStatus_PassValidForTicket_ShouldUpdateTicketOrTaskStatus()
        {
            var ticketNameHandler = new TicketNameHandler(_context);
            var ticketRepository = new TicketRepository(_context, _statusHandler, ticketNameHandler, _ticketNewValidator);
            var organizationalTaskHandler = new OrganizationalTaskStatusHandler(_context);
            var organizationalTaskRepository = new OrganizationalTaskRepository(_context, organizationalTaskHandler);
            var kanbanRepository = new KanbanRepository(ticketRepository, organizationalTaskRepository, organizationalTaskHandler);
            #region newCompany
            var companyRepository = new CompanyRepository(_context);
            CompanyDto newCompany = new CompanyDto()
            {
                CmpName = "test",
                CmpLandline = "test",
                CmpNip = "0000000000",
                CmpRegon = "",
                CmpKrsNumber = "",
                CmpNipPrefix = "tt"
            };
            await companyRepository.AddNewCompany(newCompany);
            var newCompanyId = await _context
                .Companies
                .Where(x => x.CmpNip == newCompany.CmpNip)
                .Select(x => x.CmpId)
                .SingleOrDefaultAsync();
            #endregion
            #region Adding newCustomer
            var customerHandler = new CustomerHandler(_context);
            var customerRepository = new CustomerRepository(_context, companyRepository);
            var newCustomer = new CustomerDto()
            {
                CurName = "test",
                CurComments = "test",
                CurEmail = "test@test.pl",
                CurPosition = "test",
                CurSurname = "test",
                CurPhoneNumber = "123123123"
            };
            await customerRepository.AddNewCustomer(newCustomer, newCompanyId);
            var newCustomerId = await _context
                .Customers
                .Where(x => x.CurEmail == newCustomer.CurEmail)
                .Select(x => x.CurId)
                .SingleOrDefaultAsync();
            #endregion
            var settingsRepository = new SettingRepository(_context);
            #region TicketPriority
            var newTicketPriority = new NewTicketPriorityDto()
            {
                TpiDescription = "Test",
                TpiWeight = 1
            };
            await settingsRepository.AddTicketPriority(newTicketPriority);
            var ticketPriorityId = await _context
                .TicketPriorities
                .Where(x => x.TpiDescription == newTicketPriority.TpiDescription
                            && x.TpiWeight == newTicketPriority.TpiWeight)
                .Select(x => x.TpiId)
                .SingleOrDefaultAsync();
            #endregion
            #region  TicketStatus
            var newTicketStatus = new NewTicketStatusDto()
            {
                TstDescription = "Test",
                TstName = "Test1" 
            };
            await settingsRepository.AddTicketStatus(newTicketStatus);
            var ticketStatusId = await _context
                .TicketStatuses
                .Where(x => x.TstDescription == newTicketStatus.TstDescription
                            && x.TstName == newTicketStatus.TstName)
                .Select(x => x.TstId)
                .SingleOrDefaultAsync();
            #endregion
            #region TicketType
            var newTicketType = new NewTicketTypeDto()
            {
                TtpDescription = "Test",
                TtpName = "Test"
            };
            await settingsRepository.AddTicketType(newTicketType);
            var ticketTypeId = await _context
                .TicketTypes
                .Where(x => x.TtpDescription == newTicketType.TtpDescription
                            && x.TtpName == newTicketType.TtpName)
                .Select(x => x.TtpId)
                .SingleOrDefaultAsync();
            #endregion
            #region TicketStatus_2
            var newTicketStatus2 = new NewTicketStatusDto()
            {
                TstDescription = "Test",
                TstName = "Test2" 
            };
            await settingsRepository.AddTicketStatus(newTicketStatus2);
            var ticketStatusId2 = await _context
                .TicketStatuses
                .Where(x => x.TstDescription == newTicketStatus2.TstDescription
                            && x.TstName == newTicketStatus2.TstName)
                .Select(x => x.TstId)
                .SingleOrDefaultAsync();
            #endregion
            var newTicket = new TicketNewDto()
            {
                TicTopic = "Test1",
                TicEstimatedCost = 111,
                TicDueDate = System.DateTime.Now,
                TicCompletedAt = System.DateTime.Now,
                TicDescription = "TestDesc",
                TicIdCustomer = newCustomerId,
                TicIdTicketPriority = ticketPriorityId,
                TicIdTicketStatus = ticketStatusId,
                TicIdTicketType = ticketTypeId
            };
            await ticketRepository.AddTicket(newTicket);
            var ticketId = await _context
                .Tickets
                .Where(x =>
                    x.TicTopic == newTicket.TicTopic
                    && x.TicEstimatedCost == newTicket.TicEstimatedCost
                    && x.TicDueDate == newTicket.TicDueDate
                    && x.TicCompletedAt == newTicket.TicCompletedAt
                    && x.TicDescription == newTicket.TicDescription
                    && x.TicIdCustomer == newTicket.TicIdCustomer
                    && x.TicIdTicketPriority == newTicket.TicIdTicketPriority
                    && x.TicIdTicketStatus == newTicket.TicIdTicketStatus
                    && x.TicIdTicketType == newTicket.TicIdTicketType
                )
                .Select(x => x.TicId)
                .SingleOrDefaultAsync();
            await kanbanRepository.UpdateKanbanElementStatus(ticketId, (int) KanbanType.Ticket, newTicketStatus2.TstName);
            var ticketStatusAfterUpdate = await _context
                .Tickets
                .Where(x => x.TicId == ticketId)
                .Select(x => x.TicIdTicketStatus)
                .SingleOrDefaultAsync();
            Assert.That(ticketStatusAfterUpdate, Is.EqualTo(ticketStatusId2));
            
        }
    }
}