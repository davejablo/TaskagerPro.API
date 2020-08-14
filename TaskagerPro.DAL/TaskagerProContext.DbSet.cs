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
    }
}
