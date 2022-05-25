namespace OneBan_TMS.Models.DTOs.Report
{
    public class TimeEntryReportDto
    {
        public int TerId { get; set; }
        public string TerTicketTitle { get; set; }
        public string TerDescription { get; set; }
        public string TerTimeValue { get; set; }
        public string TerDate { get; set; }
        public string TerCompany { get; set; }
    }
}