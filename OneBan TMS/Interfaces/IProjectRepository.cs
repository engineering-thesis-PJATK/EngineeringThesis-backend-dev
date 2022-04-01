using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProjectById(int pIdProject);
    }
}