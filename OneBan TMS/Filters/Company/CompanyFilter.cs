using System.Threading.Tasks;
using FluentValidation;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models.DTOs.Company;
using OneBan_TMS.Validators;
using OneBan_TMS.Validators.Interfaces;

namespace OneBan_TMS.Filters.Company
{
    public class CompanyFilter : ICompanyFilter
    {
        private readonly IValidator<CompanyDto> _companyDtoValidator;
        private readonly ICompanyHandler _companyHandler;
        public CompanyFilter(IValidator<CompanyDto> companyDtoValidator, ICompanyHandler companyHandler)
        {
            _companyDtoValidator = companyDtoValidator;
            _companyHandler = companyHandler;
        }
        public async Task<FilterResult> IsValid(CompanyDto entity)
        {
            FilterResult validationResult = await ValidationResult(entity);
            if (!(validationResult is null))
                return validationResult;
            if (await _companyHandler.IsCompanyNameUnique(entity.CmpName))
            {
                return new FilterResult()
                {
                    Message = "Company name must be unique",
                    PropertyName = "CmpName",
                    Valid = false
                };
            }
            return new FilterResult()
            {
                Valid = true
            };
        }

        public async Task<FilterResult> IsValid(CompanyDto entity, int entityId)
        {
            FilterResult validationResult = await ValidationResult(entity);
            if (!(validationResult is null))
                return validationResult;
            if (await _companyHandler.IsCompanyNameUnique(entity.CmpName, entityId))
            {
                return new FilterResult()
                {
                    Message = "Company name must be unique",
                    PropertyName = "CmpName",
                    Valid = false
                };
            }
            return new FilterResult()
            {
                Valid = true
            };
        }

        private async Task<FilterResult> ValidationResult(CompanyDto entity)
        {
            var validatorResult = await _companyDtoValidator.ValidateAsync(entity);
            if (!(validatorResult.IsValid))
            {
                return new FilterResult()
                {
                    Message = validatorResult.Errors[0].ErrorMessage,
                    PropertyName = validatorResult.Errors[0].PropertyName,
                    Valid = false
                };
            }
            return null;
        }
    }
}