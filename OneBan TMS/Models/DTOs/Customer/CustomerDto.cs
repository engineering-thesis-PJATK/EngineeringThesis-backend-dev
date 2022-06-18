using OneBan_TMS.Abstracts.Customer;
namespace OneBan_TMS.Models.DTOs.Customer
{
    using OneBan_TMS.Models;
    public class CustomerDto : CustomerAbstract
    {
        public string CurName { get; set; }
        public string CurSurname { get; set; }
        public string CurEmail { get; set; }
        public string CurPhoneNumber { get; set; }
        public string CurPosition { get; set; }
        public string CurComments { get; set; }
        public override Customer GetCustomer()
        {
            return new Customer()
            {
                CurName = this.CurName,
                CurSurname = this.CurSurname,
                CurEmail = this.CurEmail,
                CurPhoneNumber = this.CurPhoneNumber,
                CurPosition = this.CurPosition,
                CurComments = this.CurComments
            };
        }
    }
}