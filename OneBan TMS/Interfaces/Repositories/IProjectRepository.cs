using System.Collections.Generic;
using System.Threading.Tasks;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;

namespace OneBan_TMS.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(int projectId);
        Task<List<Project>> GetProjects();
        Task<IEnumerable<ProjectCompanyTeamNamesDto>> GetProjectsWithCompanyNameTeamName();
        Task<Project> AddNewProject(ProjectNewDto projectNewDto);
        Task<ProjectCompanyTeamNamesDto> GetProjectWithCompanyNameTeamNameById(int projectId);
    }
}