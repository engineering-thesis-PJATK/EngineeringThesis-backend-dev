using System.Collections;
using System.Collections.Generic;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Models;

namespace OneBan_TMS.Models.DTOs.Ticket
{
    public class TicketCustomerCompanyDto : TicketDto
    {
        public CustomerDto SingleCustomer { get; set; }
        public CompanyDto SingleCompany { get; set; }
        
    }
}