using System.Security.Principal;

namespace OneBan_TMS.Models.DTOs
{
    public class CustomerShortDto
    {
        public int CurId { get; set; }
        public string CurEmail { get; set; }
        public string CurName { get; set; }
        public string CurSurname { get; set; }
    }
}