using FluentValidation;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Validators.EmployeeValidators
{
    public class EmployeeToUpdateValidator : AbstractValidator<EmployeeToUpdate>
    {
        public EmployeeToUpdateValidator()
        {
            RuleFor(x => x.EmpLogin)
                .NotEmpty()
                .WithMessage("Login can not be empty");
            RuleFor(x => x.EmpEmail)
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(x => x.EmpName)
                .NotEmpty()
                .WithMessage("Name can not be empty");
            RuleFor(x => x.EmpSurname)
                .NotEmpty()
                .WithMessage("Surname can not be empty");
        }
    }
}