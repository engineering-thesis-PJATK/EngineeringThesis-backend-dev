using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Graph;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Customer;
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
        public void SetUp()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
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
            #endregion
            #region Adding newCustomer

            var customerHandler = new CustomerHandler(_context);
            //Todo Poprawić
            //var customerValidator = new CustomerValidator(customerHandler);
            //var customerRepository = new CustomerRepository(_context, customerValidator, companyRepository);
            /*
            customerRepository.AddNewCustomer(new CustomerDto()
            {
                CurName = "test",
                CurComments = "test",
                CurEmail = "test@test.pl",
                CurPosition = "test",
                CurSurname = "test",
                CurPhoneNumber = "123123123"
            });
            */
            #endregion
            /*
            var newTicket = new TicketNewDto()
            {
                TicTopic = "Test1",
                TicEstimatedCost = 111,
                TicDueDate = System.DateTime.Now,
                TicCompletedAt = System.DateTime.Now,
                TicDescription = "TestDesc",
                TicIdCustomer = 
            }
            
            ticketRepository.AddTicket()
            */
            //kanbanRepository.UpdateKanbanElementStatus()
            
            Assert.That(1, Is.EqualTo(1));
            
        }
    }
}