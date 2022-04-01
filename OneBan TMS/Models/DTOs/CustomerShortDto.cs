using System.Security.Principal;

namespace OneBan_TMS.Models.DTOs
{
    public class CustomerShortDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}