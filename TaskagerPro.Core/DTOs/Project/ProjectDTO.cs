using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Entities;

namespace TaskagerPro.Core.DTOs.Project
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
    }
}
