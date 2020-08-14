using System;
using System.Collections.Generic;
using System.Text;

namespace TaskagerPro.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
    }
}