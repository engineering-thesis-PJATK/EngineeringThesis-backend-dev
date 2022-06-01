using FluentValidation;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Validators.EmployeeTeamRoleValidator
{
    public class EmployeeTeamRoleValidator : AbstractValidator<EmployeeTeamRoleDto>
    {
        public EmployeeTeamRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty");
        }
    }
}