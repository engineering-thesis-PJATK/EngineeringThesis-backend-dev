using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly OneManDbContext _context;
        public TimeEntryRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<TimeEntryGetDto> GetTimeEntryById(int timeEntryId)
        {
            TimeEntry timeEntry = await _context
                .TimeEntries
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

        public async Task<List<TimeEntryGetDto>> GetTimeEntries()
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteTimeEntryById(int timeEntryId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TimeEntryGetDto> UpdateTimeEntry(int timeEntryId, TimeEntryUpdateDto timeEntryUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}