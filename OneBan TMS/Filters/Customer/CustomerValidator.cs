using System.Data;
using FluentValidation;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models.DTOs.Customer;

namespace OneBan_TMS.Filters.Customer
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
                .EmailAddress()
                .WithMessage("Email is not valid");
        }
    }
}