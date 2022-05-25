using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly OneManDbContext _context;  
        public ReportRepository(OneManDbContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<TimeEntryHeaderDto>> GetGroupDataForReport(string employeeId, string dateFrom, string dateTo, string groupPar)
        {
            throw new NotImplementedException();
        }
    }
}