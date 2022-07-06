using FluentValidation;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Validators.EmployeeValidators
{
    public class EmployeeToUpdateValidator : AbstractValidator<EmployeeToUpdateDto>
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