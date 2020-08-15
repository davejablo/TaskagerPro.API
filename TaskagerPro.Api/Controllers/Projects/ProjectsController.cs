using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Project;
using TaskagerPro.Core.Entities;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Api.Controllers.Projects
{
    [Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ?? 
                throw new ArgumentNullException(nameof(projectRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<Project>> GetProjectsAsync()
        {
            var projectsFromRepo = await _projectRepository.GetProjectsAsync();
            return Ok(projectsFromRepo);
        }

        [HttpGet("{projectId}", Name = "GetProjectById")]
        public async Task<IActionResult> GetByIdAsync(int projectId)
        {
            var projectFromRepo = await _projectRepository.GetProjectByIdAsync(projectId);
            if (projectFromRepo == null)
            {
                return NotFound();
            }
            return Ok(projectFromRepo);
        }

        [HttpPost]
        public ActionResult<AddProjectDTO> AddProject(AddProjectDTO project)
        {
            var projectEntity = _mapper.Map<Project>(project);
            _projectRepository.AddProject(projectEntity);
            _projectRepository.Save();

            var projectToReturn = _mapper.Map<ProjectDTO>(projectEntity);

            return CreatedAtRoute("GetProjectById", new { projectId = projectToReturn.ProjectId }, projectToReturn);
        }
    }
}