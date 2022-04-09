using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

namespace OneBan_TMS.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public async Task<ProjectDto> GetProjectById(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProjectDto>> GetProjects()
        {
            throw new System.NotImplementedException();
        }
    }
}