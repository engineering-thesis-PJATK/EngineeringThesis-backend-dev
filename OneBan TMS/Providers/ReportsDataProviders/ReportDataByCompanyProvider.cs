using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Strategies;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Providers.ReportsDataProvides
{
    public class ReportDataByCompanyProvider : IReportStrategy
    {
        private readonly ITimeHandler _timeHandler;
        public ReportDataByCompanyProvider(ITimeHandler timeHandler)
        {
            _timeHandler = timeHandler;
        }
        public IEnumerable<TimeEntryGroupedDto> GetReportData(IEnumerable<TimeEntryReportDto> timeEntryData)
        {
            List<TimeEntryGroupedDto> timeEntryGroupedList = new List<TimeEntryGroupedDto>();
            var groupedData = timeEntryData.GroupBy(x => x.TerCompany);
            int index = 0;
            foreach (var dataFromDate in groupedData)
            {
                timeEntryGroupedList.Add(new TimeEntryGroupedDto()
                {
                    TehId = index++,
                    TehGroupTitle = dataFromDate.Key,
                    TehGroupTimeSum = _timeHandler.GetTimeFromTicks(dataFromDate.Sum(x => x.TerTimeValue.Ticks)),
                    TehDetails = dataFromDate.ToList()
                });
            }
            return timeEntryGroupedList;
        }
    }
}