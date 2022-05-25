using System.Collections.Generic;

namespace OneBan_TMS.Models.DTOs.Report
{
    public class TimeEntryHeaderDto
    {
        public int TehId { get; set; }
        public string TehGroupTitle { get; set; }
        public string TehGroupTimeSum { get; set; }
        public IEnumerable<TimeEntryReportDto> TehDetails { get; set; }
    }
}