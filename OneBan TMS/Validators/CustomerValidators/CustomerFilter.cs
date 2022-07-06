using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models.DTOs.Customer;
using OneBan_TMS.Providers;

namespace OneBan_TMS.Validators.CustomerValidators
{
    public class CustomerFilter : ICustomerFilter
    {
        private readonly IValidator<CustomerDto> _customerValidator;
        private readonly ICustomerHandler _customerHandler;
        public CustomerFilter(IValidator<CustomerDto> customerValidator, ICustomerHandler customerHandler)
        {
            _customerValidator = customerValidator;
            _customerHandler = customerHandler;
        }

        public async Task<FilterResult> IsValid(CustomerDto entity)
        {
            var validatorResults = await _customerValidator.ValidateAsync(entity);
            if (!(validatorResults.IsValid))
            {
                return new FilterResult()
                {
                    Message = validatorResults.Errors[0].ErrorMessage,
                    PropertyName = validatorResults.Errors[0].PropertyName,
                    Valid = false
                };
            }
            if (await _customerHandler.CheckEmailUnique(entity.CurEmail))
            {
                return new FilterResult()
                {
                    Message = "Email must be unique",
                    PropertyName = "CurEmail",
                    Valid = false
                };
            }
            return new FilterResult()
            {
                Valid = true
            };
        }

        public async Task<FilterResult> IsValid(CustomerDto entity, int entityId)
        {
            var validatorResults = await _customerValidator.ValidateAsync(entity);
            if (!(validatorResults.IsValid))
            {
                return new FilterResult()
                {
                    Message = validatorResults.Errors[0].ErrorMessage,
                    PropertyName = validatorResults.Errors[0].PropertyName,
                    Valid = false
                };
            }
            if (!(await _customerHandler.CheckEmailUnique(entity.CurEmail, entityId)))
            {
                return new FilterResult()
                {
                    Message = "Email must be unique",
                    PropertyName = "CurEmail",
                    Valid = false
                };
            }
            return new FilterResult()
            {
                Valid = true
            };
        }
    }
}