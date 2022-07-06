using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Graph;
using OneBan_TMS.Models.DTOs.Employee;

namespace OneBan_TMS.Validators.EmployeeValidators
{
    public class EmployeeToAddFilter : IEmployeeToAddFilter
    {
        private readonly IValidator<EmployeeDto> _employeeToAddValidator;
        public EmployeeToAddFilter(IValidator<EmployeeDto> employeeToAddValidator)
        {
            _employeeToAddValidator = employeeToAddValidator;
        }
        public async Task<FilterResult> IsValid(EmployeeDto entity)
        {
            var validatorResults = await _employeeToAddValidator.ValidateAsync(entity);
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

        public Task<FilterResult> IsValid(EmployeeDto entity, int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}