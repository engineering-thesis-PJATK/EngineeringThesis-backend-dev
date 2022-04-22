using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<bool> ExistsCustomer(int customerId);
        Task AddNewCustomer(CustomerDto customerDto);
        Task<List<CustomerShortDto>> GetCustomersToSearch();
    }
}