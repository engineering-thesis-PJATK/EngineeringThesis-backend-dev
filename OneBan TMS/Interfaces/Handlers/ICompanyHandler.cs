using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ICompanyHandler
    {
        Task<bool> IsCompanyNameUnique(string companyName);
        Task<bool> IsCompanyNameUnique(string companyName, int companyId);
        Task<string> GetNameOfCompanyById(int companyId);
    }
}