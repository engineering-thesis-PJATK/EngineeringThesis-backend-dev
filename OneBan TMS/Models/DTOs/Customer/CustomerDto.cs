using System;
namespace OneBan_TMS.Models.DTOs.Customer
{
    using OneBan_TMS.Models;
    public class CustomerDto
    {
        public string CurName { get; set; }
        public string CurSurname { get; set; }
        public string CurEmail { get; set; }
        public string CurPhoneNumber { get; set; }
        public string CurPosition { get; set; }
        public string CurComments { get; set; }
        public Customer GetCustomer()
        {
            return new Customer()
            {
                CurName = this.CurName,
                CurSurname = this.CurSurname,
                CurEmail = this.CurEmail,
                CurPhoneNumber = this.CurPhoneNumber,
                CurPosition = this.CurPosition,
                CurComments = this.CurComments,
                CurCreatedAt = DateTime.Now
            };
        }

        public Customer GetUpdatedCustomer(Customer customer)
        {
            customer.CurName = this.CurName;
            customer.CurSurname = this.CurSurname;
            customer.CurEmail = this.CurEmail;
            customer.CurPhoneNumber = this.CurPhoneNumber;
            customer.CurPosition = this.CurPosition;
            customer.CurComments = this.CurComments;
            return customer;
        }
    }
}