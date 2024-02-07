using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<TaskDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using(var context = new TaskDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new TaskTrackingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(TaskDbContext context)
        {
            context.Roles.AddRange(
                new IdentityRole<int> { Id = 1, Name = "User", NormalizedName = "USER" },
                new IdentityRole<int> { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole<int> { Id = 3, Name = "Administrator", NormalizedName = "ADMINISTRATOR" });

            context.Users.AddRange(
                new User { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new User { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new User { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" });

            context.UserRoles.AddRange(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 3 });

            context.AssignmentStatuses.AddRange(
                new AssignmentStatus { Id = 1, Name = "Status1" },
                new AssignmentStatus { Id = 2, Name = "Status2" },
                new AssignmentStatus { Id = 3, Name = "Status3" });

            context.ProjectStatuses.AddRange(
                new ProjectStatus { Id = 1, Name = "Status1" },
                new ProjectStatus { Id = 2, Name = "Status2" },
                new ProjectStatus { Id = 3, Name = "Status3" });

            context.Projects.AddRange(
                new Project { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1 },
                new Project { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), StatusId = 2 },
                new Project { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), StatusId = 3 });

            context.Assignments.AddRange(
                new Assignment { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, ManagerId = 2, ProjectId = 1 },
                new Assignment { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), StatusId = 2, ManagerId = 2, ProjectId = 2 },
                new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), StatusId = 3, ManagerId = 2, ProjectId = 3 },
                new Assignment { Id = 4, Name = "Name4", Description = "Description4", StartDate = new DateTime(2022, 4, 1), ExpiryDate = new DateTime(2022, 4, 7), StatusId = 2, ManagerId = 2, ProjectId = 3 });

            context.Positions.AddRange(
                new Position { Id = 1, Name = "Name1", Description = "Description1" },
                new Position { Id = 2, Name = "Name2", Description = "Description2" },
                new Position { Id = 3, Name = "Name3", Description = "Description3" });

            context.UserProjects.AddRange(
                new UserProject { Id = 1, PositionId = 1, TaskId = 1, UserId = 1 },
                new UserProject { Id = 2, PositionId = 2, TaskId = 2, UserId = 1 },
                new UserProject { Id = 3, PositionId = 3, TaskId = 3, UserId = 1 },
                new UserProject { Id = 4, PositionId = 2, TaskId = 4, UserId = 1 },
                new UserProject { Id = 5, PositionId = 1, TaskId = 3, UserId = 1 });

            context.SaveChanges();
        }
    }
}
