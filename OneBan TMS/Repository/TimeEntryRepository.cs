using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.TimeEntry;

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
                                        .Where(timeEntry => timeEntry.TesId == timeEntryId)
                                        .SingleOrDefaultAsync();
            if (timeEntry is null)
            {
                return 
                    null;
            }

            return 
                ChangeTimeEntryBaseToDto(timeEntry);
        }

        public async Task<List<TimeEntryGetDto>> GetTimeEntries()
        {
            var timeEntries = await _context
                                                 .TimeEntries
                                                 .ToListAsync();
            if (!(timeEntries.Any()))
            {
                return 
                    null;
            }
            return
                timeEntries
                .Select(ChangeTimeEntryBaseToDto)
                .ToList();
        }

        public async Task DeleteTimeEntryById(int timeEntryId)
        {
            TimeEntry singleTimeEntry = await _context
                                                .TimeEntries
                                                .Where(timeEntry => timeEntry.TesId == timeEntryId)
                                                .SingleOrDefaultAsync();
                  _context
                  .TimeEntries
                  .Remove(singleTimeEntry);
            await _context
                  .SaveChangesAsync();
        }

        public async Task<TimeEntryGetDto> UpdateTimeEntry(int timeEntryId, TimeEntryUpdateDto timeEntryUpdate)
        {
            var singleTimeEntry = await _context
                                        .TimeEntries
                                        .Where(timeEntry => timeEntry.TesId.Equals(timeEntryId))
                                        .SingleOrDefaultAsync();
            if (singleTimeEntry is not null)
            {
                singleTimeEntry.TesDescription = timeEntryUpdate.TesDescription;
                singleTimeEntry.TesCreatedAt = timeEntryUpdate.TesCreatedAt;
                singleTimeEntry.TesEntryDate = timeEntryUpdate.TesEntryDate;
                singleTimeEntry.TesEntryTime = timeEntryUpdate.TesEntryTime;
                singleTimeEntry.TesIdTicket = timeEntryUpdate.TesIdTicket;
                singleTimeEntry.TesUpdatedAt = timeEntryUpdate.TesUpdatedAt;
                singleTimeEntry.TesIdProjectTask = timeEntryUpdate.TesIdProjectTask;
                
                await _context
                    .SaveChangesAsync();
                
                return 
                    GetTimeEntryById(timeEntryId)
                    .Result;
            }

            return 
                null;
        }

        public async Task<List<TimeEntryGetDto>> GetTimeEntriesByTicketId(int ticketId)
        {
            var timeEntries = await _context
                .TimeEntries.Where(timeEntry => timeEntry.TesIdTicket == ticketId)
                .ToListAsync();
            if (!(timeEntries.Any()))
            {
                return 
                    null;
            }
            return
                timeEntries
                    .Select(ChangeTimeEntryBaseToDto)
                    .ToList();
        }

        private TimeEntryGetDto ChangeTimeEntryBaseToDto(TimeEntry timeEntry)
        {
            return
                new TimeEntryGetDto()
                {
                    TesId = timeEntry.TesId,
                    TesDescription = timeEntry.TesDescription,
                    TesCreatedAt = timeEntry.TesCreatedAt,
                    TesEntryDate = timeEntry.TesEntryDate,
                    TesEntryTime = timeEntry.TesEntryTime,
                    TesIdTicket = timeEntry.TesIdTicket,
                    TesUpdatedAt = timeEntry.TesUpdatedAt,
                    TesIdProjectTask = timeEntry.TesIdProjectTask
                };
        }
    }
}