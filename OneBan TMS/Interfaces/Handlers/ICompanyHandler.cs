using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces.Handlers
{
    public interface ICompanyHandler
    {
        Task<bool> UniqueCompanyName(string companyName);
    }
}