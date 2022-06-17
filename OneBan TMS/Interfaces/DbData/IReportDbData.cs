using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Interfaces.DbData
{
    public interface IReportDbData
    { 
        Task<IEnumerable<TimeEntryReportDto>> GetDataFromDb(int employeeId, DateTime dateFrom, DateTime dateTo);
    }
}