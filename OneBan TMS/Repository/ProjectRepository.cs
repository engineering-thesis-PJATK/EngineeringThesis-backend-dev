using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Interfaces.Repositories;
using OneBan_TMS.Models;
using OneBan_TMS.Models.DTOs;

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
    }
}