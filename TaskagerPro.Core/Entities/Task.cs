using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.Enums;
using TaskagerPro.Core.Identities;

namespace TaskagerPro.Core.Entities
{
    public class Task
    {   
        public int TaskId { get; set; }
        public int UserProjectId { get; set; }
        public UserProject UserProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }
        public int HoursSpent { get; set; }
        public decimal TaskCost { get; set; }
        public StatusTask Status { get; set; }
        public TaskPriority Priority { get; set; }
        public bool Is_Done { get; set; }
        public DateTime DoneAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}