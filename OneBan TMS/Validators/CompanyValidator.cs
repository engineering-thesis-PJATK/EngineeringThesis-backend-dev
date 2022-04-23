using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Handlers;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Handlers;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Company;

namespace OneBan_TMS.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        private readonly ICompanyHandler _companyHandler;
        public CompanyValidator(ICompanyHandler companyHandler)
        {
            _companyHandler = companyHandler;
            RuleFor(x => x.CmpName)
                .MustAsync(async (x, y) => !(await _companyHandler.UniqueCompanyName(x)))
                .WithMessage("Company name must be unique");
            RuleFor(x => x.CmpNipPrefix)
                .Length(2)
                .WithMessage("Nip prefix must contains 2 signs");
        }
    }
}