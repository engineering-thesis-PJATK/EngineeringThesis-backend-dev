using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(int projectId);
        Task<List<Project>> GetProjects();
    }
}