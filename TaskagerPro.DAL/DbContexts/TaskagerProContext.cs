using Microsoft.AspNetCore.Identity;
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

            modelBuilder.Entity<ApplicationUser>()
                .Property(a => a.Sex)
                .HasMaxLength(1)
                .HasDefaultValue(null);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectId);
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserId);
            
            modelBuilder.Entity<AccountType>()
                .HasData(new AccountType { Id = 1, Name = "Admin", Description = "Admin" },
                         new AccountType { Id = 2, Name = "Leader", Description = "Leader" },
                         new AccountType { Id = 3, Name = "Worker", Description = "Worker" }
                         );
        }
    }
}