using System.Collections.Generic;
using OneBan_TMS.Models;

namespace OneBan_TMS.Interfaces
{
    public interface IProject
    {
        Project GetProjectById(int pProjectId);
        IList<Project> GetAllProjects();
    }
}