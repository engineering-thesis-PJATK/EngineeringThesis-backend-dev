using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;
using OneBan_TMS.Models.DTOs.Project;

namespace OneBan_TMS.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly OneManDbContext _context;
        public ProjectRepository(OneManDbContext context)
        {
            _context = context;
        }
        public async Task<Project> GetProjectById(int projectId)
        {
            Project project = await _context
                .Projects
                .Where(project =>project.ProId == projectId)
                .SingleOrDefaultAsync();
            if (project is null)
            {
                return
                    null;
            }

            return
                project;
        }
        
        public async Task<List<Project>> GetProjects()
        {
            var projects = await _context
                .Projects
                .ToListAsync();
            if (!(projects.Any()))
            {
                return 
                    null;
            }

            return
                projects;

        }

        public async Task<IEnumerable<ProjectCompanyTeamNamesDto>> GetProjectsWithCompanyNameTeamName()
        {
            List<ProjectCompanyTeamNamesDto> projectCompanyTeamNamesDtoList = new List<ProjectCompanyTeamNamesDto>();
            var allProjectsData = await _context.Projects
                .Include(x => x.ProIdCompanyNavigation)
                .Include(x => x.ProIdTeamNavigation)
                .ToListAsync();
            foreach (Project project in allProjectsData)
            {
                projectCompanyTeamNamesDtoList.Add(project.GetProjectCompanyTeamNamesDto());
            }
            return projectCompanyTeamNamesDtoList;
        }

        public async Task<ProjectCompanyTeamNamesDto> GetProjectWithCompanyNameTeamNameById(int projectId)
        {
            var projectData = await _context.Projects
                .Include(x => x.ProIdCompanyNavigation)
                .Include(x => x.ProIdTeamNavigation)
                .Where(x => x.ProId == projectId)
                .SingleOrDefaultAsync();
            return projectData.GetProjectCompanyTeamNamesDto();
        }

        public async Task<Project> AddNewProject(ProjectNewDto projectNewDto)
        {
            var newProject = projectNewDto.GetProject();
            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }
    }
}