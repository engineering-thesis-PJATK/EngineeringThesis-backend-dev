using System;
using OneBan_TMS.Enum;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Interfaces.Repositories.IReportStrategy;
using OneBan_TMS.Repository.ReportStrategy;

namespace OneBan_TMS.Providers
{
    public static class ReportDataStrategyProvider
    {
        public static IReportStrategy GetReportStrategy(int groupType, IReportHandler reportHandler)
        {
            switch (groupType)
            {
                case (int)ReportGroupType.Date:
                    return new DateGroupReportData(reportHandler);
                    break;
                case (int)ReportGroupType.Company:
                    return new CompanyGroupReportData(reportHandler);
                    break;
                default:
                    throw new ArgumentException($"Group type with number {groupType} does not exist");
            }
        }
    }
}