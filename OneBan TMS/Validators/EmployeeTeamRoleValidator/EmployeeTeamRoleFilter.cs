using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Models;

namespace OneBan_TMS.Validators.EmployeeTeamRoleValidator
{
    public class EmployeeTeamRoleFilter :  IEmployeeTeamRoleFilter
    {
        private readonly IValidator<EmployeeTeamRole> _employeeTeamRoleValidator;
        public EmployeeTeamRoleFilter(IValidator<EmployeeTeamRole> employeeTeamRoleValidator)
        {
            _employeeTeamRoleValidator = employeeTeamRoleValidator;
        }
        public async Task<FilterResult> IsValid(EmployeeTeamRole entity)
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
            return new FilterResult()
            {
                Valid = true
            };
        }

        public Task<FilterResult> IsValid(EmployeeTeamRole entity, int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}