using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }
        
        public DbSet<Assignment> Assignments { get; set; }
        
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        
        public DbSet<Position> Positions { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        
        public DbSet<UserProject> UserProjects { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProject>()
                .HasIndex(t => new { t.TaskId, t.UserId })
                .IsUnique();

            modelBuilder.Entity<ProjectStatus>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<AssignmentStatus>()
                .HasIndex(u => u.Name)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
