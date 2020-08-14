using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Identities;

namespace TaskagerPro.Core.Entities
{
    public class UserProject
    {
        public int UserProjectId { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}