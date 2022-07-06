using System;
using System.Collections.Generic;

#nullable disable

namespace OneBan_TMS.Models
{
    using Models.DTOs.Customer;
    public partial class Customer
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int CurId { get; set; }
        public string CurName { get; set; }
        public string CurSurname { get; set; }
        public string CurEmail { get; set; }
        public string CurPhoneNumber { get; set; }
        public string CurPosition { get; set; }
        public string CurComments { get; set; }
        public DateTime CurCreatedAt { get; set; }
        public int CurIdCompany { get; set; }

        public virtual Company CurIdCompanyNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public CustomerShortDto GetCustomerShortDto()
        {
            return new CustomerShortDto()
            {
                CurId = this.CurId,
                CurEmail = this.CurEmail,
                CurName = this.CurName,
                CurSurname = this.CurSurname
            };
        }

        public CustomerCompanyNameDto GetCustomerCompanyNameDto(string companyName)
        {
            return new CustomerCompanyNameDto()
            {
                CurId = this.CurId,
                CurName = this.CurName,
                CurSurname = this.CurSurname,
                CurEmail = this.CurEmail,
                CurPhoneNumber = this.CurPhoneNumber,
                CurPosition = this.CurPosition,
                CurComments = this.CurComments,
                CurCreatedAt = this.CurCreatedAt,
                CurIdCompany = this.CurIdCompany,
                CurCompanyName = companyName
            };
        }
    }
}
