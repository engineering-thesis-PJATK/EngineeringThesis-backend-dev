using System;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Strategies;
using OneBan_TMS.Providers.ReportsDataProvides;

namespace OneBan_TMS.Providers
{
    public static class ReportDataStrategyProvider
    {
        public static IReportStrategy GetReportStrategy(int groupType, ITimeHandler timeHandler)
        {
            switch (groupType)
            {
                case (int)ReportGroupType.Date:
                    return new ReportDataByDateProvider(timeHandler);
                    break;
                case (int)ReportGroupType.Company:
                    return new ReportDataByCompanyProvider(timeHandler);
                    break;
                default:
                    throw new ArgumentException($"Group type with number {groupType} does not exist");
            }
        }
    }
}