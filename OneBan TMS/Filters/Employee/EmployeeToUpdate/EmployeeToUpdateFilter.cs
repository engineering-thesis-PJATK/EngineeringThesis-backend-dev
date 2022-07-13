using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Models.DTOs.Employee;
using OneBan_TMS.Validators;

namespace OneBan_TMS.Filters.Employee.EmployeeToUpdate
{
    public class EmployeeToUpdateFilter : IEmployeeToUpdateFilter
    {
        private readonly IValidator<EmployeeToUpdateDto> _employeeToUpdateValidator;
        public EmployeeToUpdateFilter(IValidator<EmployeeToUpdateDto> employeeToUpdateValidator)
        {
            _employeeToUpdateValidator = employeeToUpdateValidator;
        }
        public async Task<FilterResult> IsValid(EmployeeToUpdateDto entity)
        {
            var validatorResults = await _employeeToUpdateValidator.ValidateAsync(entity);
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
        public Task<FilterResult> IsValid(EmployeeToUpdateDto entity, int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}