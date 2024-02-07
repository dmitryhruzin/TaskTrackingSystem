using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBase
{
    /// <summary>
    ///   Implements identitydbcontext
    /// </summary>
    public class TaskDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        /// <summary>Initializes a new instance of the <see cref="TaskDbContext" /> class.</summary>
        /// <param name="options">The options.</param>
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {

        }

        /// <summary>Gets or sets the assignments.</summary>
        /// <value>The assignments.</value>
        public DbSet<Assignment> Assignments { get; set; }

        /// <summary>Gets or sets the assignment statuses.</summary>
        /// <value>The assignment statuses.</value>
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }

        /// <summary>Gets or sets the positions.</summary>
        /// <value>The positions.</value>
        public DbSet<Position> Positions { get; set; }

        /// <summary>Gets or sets the projects.</summary>
        /// <value>The projects.</value>
        public DbSet<Project> Projects { get; set; }

        /// <summary>Gets or sets the project statuses.</summary>
        /// <value>The project statuses.</value>
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        /// <summary>Gets or sets the user projects.</summary>
        /// <value>The user projects.</value>
        public DbSet<UserProject> UserProjects { get; set; }

        //public TaskDbContext() : base()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-SP1DHKR\SQLFORMAX;Database=TaskTracking;Trusted_Connection=True;");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<UserProject>()
        //        .HasAlternateKey(t => new { t.TaskId, t.UserId });

        //    modelBuilder.Entity<ProjectStatus>()
        //        .HasAlternateKey(t => t.Name);

        //    modelBuilder.Entity<AssignmentStatus>()
        //        .HasAlternateKey(t => t.Name);
        //}
    }
}
