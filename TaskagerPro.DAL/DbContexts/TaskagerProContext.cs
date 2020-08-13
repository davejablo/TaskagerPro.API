﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Entities;
using TaskagerPro.Core.Identities;

namespace TaskagerPro.DAL
{
    public partial class TaskagerProContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TaskagerProContext(DbContextOptions<TaskagerProContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.AccountType)
                .WithMany(t => t.ApplicationUsers)
                .HasForeignKey(u => u.AccountTypeId);

            modelBuilder.Entity<AccountType>()
                .HasData(new AccountType { Id = 1, Name = "Admin", Description = "Admin" },
                         new AccountType { Id = 2, Name = "Leader", Description = "Leader" },
                         new AccountType { Id = 3, Name = "Worker", Description = "Worker" }
                         );
        }
    }
}
