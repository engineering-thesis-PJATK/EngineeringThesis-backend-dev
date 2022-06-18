using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.DbData;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Interfaces.Repositories.IReportStrategy;
using OneBan_TMS.Models.DTOs.Report;
using OneBan_TMS.Providers;
using OneBan_TMS.Repository.ReportStrategy;

namespace OneBan_TMS.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportDbData _reportDbData;
        private readonly IReportHandler _reportHandler;
        private IReportStrategy _reportStrategy;
        public ReportRepository(IReportDbData reportDbData, IReportHandler reportHandler)
        {
            _reportDbData = reportDbData;
            _reportHandler = reportHandler;
        }
        public async Task<IEnumerable<TimeEntryHeaderDto>> GetGroupDataForReport(int employeeId, DateTime dateFrom, DateTime dateTo, int groupType)
        {
            IEnumerable<TimeEntryReportDto> reportData = await _reportDbData.GetDataFromDb(employeeId, dateFrom, dateTo);
            _reportStrategy = ReportDataStrategyProvider.GetReportStrategy(groupType, _reportHandler);
            var data = _reportStrategy.GetReportData(reportData);
            return data;
        }
    }
}