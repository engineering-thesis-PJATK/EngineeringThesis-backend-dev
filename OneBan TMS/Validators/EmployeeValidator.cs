using FluentValidation;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private readonly IValidatorHandler _validatorHandler;
        public EmployeeValidator(IValidatorHandler validatorHandler)
        {
            _validatorHandler = validatorHandler;
            
            RuleFor(x => x.EmpLogin)
                .NotEmpty()
                .WithMessage("Login can not be empty");
            RuleFor(x => x.EmpEmail)
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(x => x.EmpPassword)
                .MinimumLength(5)
                .WithMessage("Password must contains more than 5 signs")
                .Must(x => !(_validatorHandler.ExistsUpperCharacters(x)))
                .WithMessage("Password must contains at least one upper character")
                .Must(x => !(_validatorHandler.ExistsSpecialSigns(x)))
                .WithMessage("Password must contains at least one special signs")
                .Must(x => !(_validatorHandler.ExistsNumbers(x)))
                .WithMessage("Password must at least one number ");
            RuleFor(x => x.EmpName)
                .Empty()
                .WithMessage("Name can not be empty");
            RuleFor(x => x.EmpSurname)
                .Empty()
                .WithMessage("Surname can not be empty");
        }
    }
}