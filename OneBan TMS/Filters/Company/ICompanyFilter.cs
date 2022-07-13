using System.Threading.Tasks;
using OneBan_TMS.Models.DTOs.Company;

namespace OneBan_TMS.Validators.Interfaces
{
    public interface ICompanyFilter : IBaseFilter<CompanyDto>
    {
        abstract Task<FilterResult> IsValid(CompanyDto entity, int entityId);
    }
}