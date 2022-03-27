using System.Collections.Generic;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        void AddNewCustomer();
    }
}