using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Project;
using TaskagerPro.Core.Entities;

namespace TaskagerPro.Services.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int projectId);
        void AddProject(Project model);
        bool Save();
    }
}
