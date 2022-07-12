using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Interfaces.Strategies
{
    public interface IReportStrategy
    {
        IEnumerable<TimeEntryGroupedDto> GetReportData(IEnumerable<TimeEntryReportDto> timeEntryData);
    }
}