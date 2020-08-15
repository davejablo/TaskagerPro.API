using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Project;
using TaskagerPro.Core.Entities;
using TaskagerPro.DAL;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Services.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskagerProContext _dbContext;

        public ProjectRepository(TaskagerProContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projects = await _dbContext.Projects.ToListAsync();
            return projects;
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.ProjectId == projectId);
            return project;
        }

        public void AddProject(Project model)
        {
            var projectExists = _dbContext.Projects.Any(p => p.Name == model.Name);

            if(projectExists)
                throw new InvalidOperationException("This product already exist.");

            var newProject = new Project
            {
                Name = model.Name,
                Budget = model.Budget,
                CreatedAt = DateTime.Today
            };

            _dbContext.Projects.Add(newProject);
        }

        public bool Save()
        {
            return (_dbContext.SaveChanges() >= 0);
        }
    }
}
