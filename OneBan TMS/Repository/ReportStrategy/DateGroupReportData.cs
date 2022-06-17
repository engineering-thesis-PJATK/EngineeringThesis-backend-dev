using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Interfaces.Repositories.IReportStrategy;
using OneBan_TMS.Models.DTOs.Report;

namespace OneBan_TMS.Repository.ReportStrategy
{
    public class DateGroupReportData : IReportStrategy
    {
        private readonly IReportHandler _reportHandler;
        public DateGroupReportData(IReportHandler reportHandler)
        {
            _reportHandler = reportHandler;
        }
        public IEnumerable<TimeEntryHeaderDto> GetReportData(IEnumerable<TimeEntryReportDto> timeEntryData)
        {
            List<TimeEntryHeaderDto> headerReportList = new List<TimeEntryHeaderDto>();
            var groupingBase = timeEntryData.Select(x => x.TerDate).Distinct();
            int index = 0;
            foreach (var date in groupingBase)
            {
                var ticksVal = timeEntryData
                    .Where(x => 
                        x.TerDate == date)
                    .Sum(x => x.TerTimeValue.Ticks);
                headerReportList.Add(new()
                {
                    TehId = index++,
                    TehGroupTitle = date.ToString("yyyy MMMM dd"),
                    TehGroupTimeSum = _reportHandler.GetTimeFromTicks(ticksVal),
                    TehDetails = timeEntryData
                        .Where(x => 
                            x.TerDate == date)
                        .ToList()
                });
            }
            return headerReportList;
        }
    }
}