using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories.IReportStrategy;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Repository.ReportStrategy
{
    public class CompanyGroupReportData : IReportStrategy
    {
        private readonly IReportHandler _reportHandler;
        public CompanyGroupReportData(IReportHandler reportHandler)
        {
            _reportHandler = reportHandler;
        }
        public IEnumerable<TimeEntryHeaderDto> GetReportData(IEnumerable<TimeEntryReportDto> timeEntryData)
        {
            throw new System.NotImplementedException();
        }
    }
}