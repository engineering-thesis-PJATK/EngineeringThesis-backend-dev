using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Validators;
using OneBan_TMS.Validators.EmployeeTeamRoleValidator;

namespace OneBan_TMS.Filters.EmployeeTeamRole
{
    using Models;
    public class EmployeeTeamRoleFilter :  IEmployeeTeamRoleFilter
    {
        private readonly IValidator<EmployeeTeamRole> _employeeTeamRoleValidator;
        public EmployeeTeamRoleFilter(IValidator<EmployeeTeamRole> employeeTeamRoleValidator)
        {
            _employeeTeamRoleValidator = employeeTeamRoleValidator;
        }
        public async Task<FilterResult> IsValid(EmployeeTeamRole entity)
        {
            FilterResult validationResult = await ValidationResult(entity);
            if (!(validationResult is null))
                return validationResult;
            return new FilterResult()
            {
                Valid = true
            };
        }
        private async Task<FilterResult> ValidationResult(EmployeeTeamRole entity)
        {
            var validatorResults = await _employeeTeamRoleValidator.ValidateAsync(entity);
            if (!(validatorResults.IsValid))
            {
                return new FilterResult()
                {
                    Message = validatorResults.Errors[0].ErrorMessage,
                    PropertyName = validatorResults.Errors[0].PropertyName,
                    Valid = false
                };
            }
            return null;
        }
    }
}