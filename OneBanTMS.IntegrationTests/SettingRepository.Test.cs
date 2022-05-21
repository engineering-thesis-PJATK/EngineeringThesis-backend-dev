using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OneBan_TMS.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Setting;
using OneBan_TMS.Models.DTOs.Ticket;
using OneBan_TMS.Repository;
using OneBan_TMS.Validators.TicketValidators;

namespace OneBanTMS.IntegrationTests
{
    public class SettingRepository_Test
    {
        private OneManDbContext _context;
        private SettingRepository _settingRepository;
        [SetUp]
        public void Init()
        {
            _context = DbContextFactory.GetOneManDbContext();
            _settingRepository = new SettingRepository(_context);
        }
        [Test, Isolated]
        public async Task AddTicketPriority_PassValid_ShouldAddTicketPriorityToDatabase()
        {
            NewTicketPriorityDto newTicketPriorityDto = new NewTicketPriorityDto()
            {
                TpiDescription = "Test1",
                TpiWeight = 1
            };
            await _settingRepository.AddTicketPriority(newTicketPriorityDto);
            var resultCount = await _context
                .TicketPriorities
                .CountAsync(x => x.TpiDescription == newTicketPriorityDto.TpiDescription
                                 && x.TpiWeight == newTicketPriorityDto.TpiWeight);
            Assert.That(resultCount, Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task AddTicketType_PassValid_ShouldAddTicketTypeToDatabase()
        {
            NewTicketTypeDto newTicketTypeDto = new NewTicketTypeDto()
            {
                TtpDescription = "Test1",
                TtpName = "test2"
            };
            await _settingRepository.AddTicketType(newTicketTypeDto);
            var resultCount = await _context
                .TicketTypes
                .CountAsync(x => x.TtpDescription == newTicketTypeDto.TtpDescription
                                 && x.TtpName == newTicketTypeDto.TtpName);
            Assert.That(resultCount, Is.EqualTo(1));
        }
        [Test, Isolated]
        public async Task AddTicketStatus_PassValid_ShouldAddTicketStatusToDatabase()
        {
            NewTicketStatusDto newTicketStatusDto = new NewTicketStatusDto()
            {
                TstDescription = "Test1",
                TstName = "Test1"
            };
            await _settingRepository.AddTicketStatus(newTicketStatusDto);
            var resultCount = await _context
                .TicketStatuses
                .CountAsync(x => x.TstDescription == newTicketStatusDto.TstDescription
                                 && x.TstName == newTicketStatusDto.TstName);
            Assert.That(resultCount, Is.EqualTo(1));
        }
    }
}