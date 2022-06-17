using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<bool> ExistsCustomer(int customerId);
        Task<Customer> AddNewCustomer(CustomerDto customerDto, int companyId);
        Task UpdateCustomer(CustomerDto customerDto, int customerId);
        Task<List<CustomerShortDto>> GetCustomersToSearch();
        Task<List<CustomerCompanyNameDto>> GetCustomersWithCompanyName();
        Task<CustomerCompanyNameDto> GetCustomerWithCompanyName(int customerId);
        Task DeleteCustomer(int customerId);
    }
}