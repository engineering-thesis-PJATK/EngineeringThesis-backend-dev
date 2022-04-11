using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntryGetDto> GetTimeEntryById(int timeEntryId);
        Task<List<TimeEntryGetDto>> GetTimeEntries();
        Task DeleteTimeEntryById(int timeEntryId);
        Task<TimeEntryGetDto> UpdateTimeEntry(int timeEntryId, TimeEntryUpdateDto timeEntryUpdate);
    }
}