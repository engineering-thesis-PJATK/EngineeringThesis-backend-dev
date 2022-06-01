using System.Collections.Generic;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Models.DTOs.Ticket
{
    public class CustomTicketById : TicketDto
    {
        public CustomerDto SingleCustomer { get; set; }
        public CompanyDto SingleCompany { get; set; }
        public IEnumerable<TicketStatusDto> TicketStatuses { get; set; }
        public IEnumerable<TicketTypeDto> TicketTypes { get; set; }
    }
}