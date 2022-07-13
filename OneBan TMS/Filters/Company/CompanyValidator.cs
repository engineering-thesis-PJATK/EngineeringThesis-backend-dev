using System;
using System.Data;
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

namespace OneBan_TMS.Filters.Company
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        private readonly ICompanyHandler _companyHandler;
        public CompanyValidator(ICompanyHandler companyHandler)
        {
            _companyHandler = companyHandler;
            RuleFor(x => x.CmpName)
                .NotEmpty()
                .WithMessage("Company name must not be empty");
            RuleFor(x => x.CmpNipPrefix)
                .Length(2)
                .WithMessage("Nip prefix must contains 2 signs");
        }
    }
}