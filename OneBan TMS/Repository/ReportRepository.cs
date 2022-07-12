using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.DbData;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Interfaces.Strategies;
using OneBan_TMS.Models.DTOs.Report;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportDbData _reportDbData;
        private readonly ITimeHandler _timeHandler;
        private IReportStrategy _reportStrategy;
        public ReportRepository(IReportDbData reportDbData, ITimeHandler timeHandler)
        {
            _reportDbData = reportDbData;
            _timeHandler = timeHandler;
        }
        public async Task<IEnumerable<TimeEntryGroupedDto>> GetGroupDataForReport(int employeeId, DateTime dateFrom, DateTime dateTo, int groupType)
        {
            IEnumerable<TimeEntryReportDto> reportData = await _reportDbData.GetDataFromDb(employeeId, dateFrom, dateTo);
            _reportStrategy = ReportDataStrategyProvider.GetReportStrategy(groupType, _timeHandler);
            var data = _reportStrategy.GetReportData(reportData);
            return data;
        }
    }
}