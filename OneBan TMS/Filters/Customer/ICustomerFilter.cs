using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Validators;

namespace OneBan_TMS.Filters.Customer
{
    public interface ICustomerFilter : IBaseFilter<CustomerDto>
    {
        Task<FilterResult> IsValid(CustomerDto entity, int entityId);
    }
}