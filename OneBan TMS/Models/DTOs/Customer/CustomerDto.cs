using System.Reflection.Metadata.Ecma335;

namespace OneBan_TMS.Models.DTOs.Customer
{
    public class CustomerDto
    {
        public string CurName { get; set; }
        public string CurSurname { get; set; }
        public string CurEmail { get; set; }
        public string CurPhoneNumber { get; set; }
        public string CurPosition { get; set; }
        public string CurComments { get; set; }
    }
}