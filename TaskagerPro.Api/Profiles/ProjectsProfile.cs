using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Project;
using TaskagerPro.Core.Entities;

namespace TaskagerPro.Api.Profiles
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<AddProjectDTO, Project>();
        }
    }
}
