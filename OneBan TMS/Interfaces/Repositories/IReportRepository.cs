using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<TimeEntryHeaderDto>> GetGroupDataForReport(int employeeId, DateTime dateFrom, DateTime dateTo, int groupType);
    }
}