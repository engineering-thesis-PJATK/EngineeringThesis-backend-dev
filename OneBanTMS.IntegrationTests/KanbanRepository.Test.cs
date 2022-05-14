using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Graph;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Repository;

namespace OneBanTMS.IntegrationTests
{
    public class KanbanRepository_Test
    {
        private readonly OneManDbContext _context;
        public KanbanRepository_Test()
        {
            var connectionString = "Server=tcp:pjwstkinzynierka.database.windows.net,1433;Initial Catalog=inzynierka;Persist Security Info=False;User ID=Hydra;Password=RUCH200nowe;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<OneManDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            _context = new OneManDbContext(optionBuilder.Options);
        }
        public async Task UpdateKanbanElementStatus_PassValidForTicket_ShouldUpdateTicketOrTaskStatus()
        {
          /*  var statusHandler = new StatusHandler(_context);
            var ticketRepository = new TicketRepository(_context, statusHandler);
            var organizationalTaskRepository = new OrganizationalTaskRepository(_context);
            var kanbanRepository = new KanbanRepository(ticketRepository, organizationalTaskRepository);
                //ticketRepository.
            Assert.That(1, Is.EqualTo(1));
            */
          //Todo: zamienić
        }
    }
}