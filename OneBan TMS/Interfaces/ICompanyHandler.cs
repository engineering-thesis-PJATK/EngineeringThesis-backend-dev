using System.Threading.Tasks;

namespace OneBan_TMS.Interfaces
{
    public interface ICompanyHandler
    {
        Task<bool> UniqueCompanyName(string companyName);
    }
}