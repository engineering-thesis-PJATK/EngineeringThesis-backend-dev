using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectDto> GetProjectById(int projectId);
        Task<List<ProjectDto>> GetProjects();
    }
}