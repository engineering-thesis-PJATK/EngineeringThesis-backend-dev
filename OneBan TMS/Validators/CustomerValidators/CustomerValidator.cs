using System.Data;
using FluentValidation;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Validators.CustomerValidators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        private readonly ICustomerHandler _customerHandler;
        public CustomerValidator(ICustomerHandler customerHandler)
        {
            _customerHandler = customerHandler;
            RuleFor(x => x.CurEmail)
                .NotEmpty()
                .WithMessage("Email can not be empty")
                .MustAsync(async (x, y) => await _customerHandler.UniqueCustomerEmail(x))
                .WithMessage("Customer email must be unique")
                .EmailAddress()
                .WithMessage("Email is not valid");
        }
    }
}