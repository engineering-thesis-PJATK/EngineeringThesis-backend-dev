using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Interfaces.Repositories.IReportStrategy
{
    public interface IReportStrategy
    {
        IEnumerable<TimeEntryHeaderDto> GetReportData(IEnumerable<TimeEntryReportDto> timeEntryData);
    }
}