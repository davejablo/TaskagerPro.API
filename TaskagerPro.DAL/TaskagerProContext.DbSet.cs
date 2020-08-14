using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Entities;

namespace TaskagerPro.DAL
{
    public partial class TaskagerProContext
    {
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}