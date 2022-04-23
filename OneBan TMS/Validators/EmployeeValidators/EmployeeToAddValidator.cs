using FluentValidation;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Validators.EmployeeValidators
{
    public class EmployeeToAddValidator : AbstractValidator<EmployeeDto>
    {
        private readonly IValidatorHandler _validatorHandler;
        public EmployeeToAddValidator(IValidatorHandler validatorHandler)
        {
            _validatorHandler = validatorHandler;
            
            RuleFor(x => x.EmpEmail)
                .NotEmpty()
                .WithMessage("Login can not be empty");
            RuleFor(x => x.EmpEmail)
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(x => x.EmpPassword)
                .MinimumLength(5)
                .WithMessage("Password must contains more than 5 signs")
                .Must(x => (_validatorHandler.ExistsUpperCharacters(x)))
                .WithMessage("Password must contains at least one upper character")
                .Must(x => (_validatorHandler.ExistsSpecialSigns(x)))
                .WithMessage("Password must contains at least one special signs")
                .Must(x => (_validatorHandler.ExistsNumbers(x)))
                .WithMessage("Password must at least one number ");
            RuleFor(x => x.EmpName)
                .NotEmpty()
                .WithMessage("Name can not be empty");
            RuleFor(x => x.EmpSurname)
                .NotEmpty()
                .WithMessage("Surname can not be empty");
        }
    }
}