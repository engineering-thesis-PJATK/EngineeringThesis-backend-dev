using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        public async Task<TimeEntryGetDto> GetTimeEntryById(int timeEntryId)
        {
            throw new System.NotImplementedException();
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