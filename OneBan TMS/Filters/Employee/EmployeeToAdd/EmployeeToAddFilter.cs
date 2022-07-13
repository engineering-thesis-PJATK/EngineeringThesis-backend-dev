using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Validators;

namespace OneBan_TMS.Filters.Employee.EmployeeToAdd
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
            FilterResult validationResult = await ValidationResult(entity);
            if (!(validationResult is null))
                return validationResult;
            return new FilterResult()
            {
                Valid = true
            };
        }
        private async Task<FilterResult> ValidationResult(EmployeeDto entity)
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
            return null;
        }
    }
}