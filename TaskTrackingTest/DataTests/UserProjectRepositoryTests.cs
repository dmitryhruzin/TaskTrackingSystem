using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.DataTests
{
    [TestFixture]
    public class UserProjectRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task UserProjectRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            var expected = ExpectedUserProjects.FirstOrDefault(x => x.Id == id);

            //Act
            var actual = await userProjectRepository.GetByIdAsync(id);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new UserProjectEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task UserProjectRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            //Act
            var actual = await userProjectRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(ExpectedUserProjects).Using(new UserProjectEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task UserProjectRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            var userProject = new UserProject { Id = 6, TaskId = 3, PositionId = 2, UserId = 1 };

            //Act
            await userProjectRepository.AddAsync(userProject);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.UserProjects.Count(), Is.EqualTo(6), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserProjectRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            //Act
            await userProjectRepository.DeleteByIdAsync(1);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.UserProjects.Count(), Is.EqualTo(4), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task UserProjectRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            var userProject = new UserProject
            {
                Id = 1,
                TaskId = 3,
                PositionId = 2,
                UserId = 1
            };

            //Act
            userProjectRepository.Update(userProject);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(userProject, Is.EqualTo(new UserProject
            {
                Id = 1,
                TaskId = 3,
                PositionId = 2,
                UserId = 1
            }).Using(new UserProjectEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task UserProjectRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var userProjectRepository = new UserProjectRepository(context);

            //Act
            var actual = await userProjectRepository.GetAllWithDetailsAsync();

            //Assert
            Assert.That(actual,
                Is.EqualTo(ExpectedUserProjects).Using(new UserProjectEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(actual.Select(i => i.Position).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedPositions).Using(new PositionEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.User).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers.Where(i => i.Id == 1)).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Task).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedTasks).Using(new AssignmentEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Task.Manager).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers.Where(i => i.Id == 2)).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Task.Status).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedAssignmentStatuses).Using(new AssignmentStatusEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Task.Project).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedProjects).Using(new ProjectEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Task.Project.Status).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedProjectStatuses).Using(new ProjectStatusEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
        }

        private static IEnumerable<Project> ExpectedProjects =>
            new[]
            {
                new Project { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1 },
                new Project { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), StatusId = 2 },
                new Project { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), StatusId = 3 }
            };

        private static IEnumerable<AssignmentStatus> ExpectedAssignmentStatuses =>
            new[]
            {
                new AssignmentStatus { Id = 1, Name = "Status1" },
                new AssignmentStatus { Id = 2, Name = "Status2" },
                new AssignmentStatus { Id = 3, Name = "Status3" }
            };

        private static IEnumerable<Assignment> ExpectedTasks =>
            new[]
            {
                new Assignment { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, ManagerId = 2, ProjectId = 1 },
                new Assignment { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), StatusId = 2, ManagerId = 2, ProjectId = 2 },
                new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), StatusId = 3, ManagerId = 2, ProjectId = 3 },
                new Assignment { Id = 4, Name = "Name4", Description = "Description4", StartDate = new DateTime(2022, 4, 1), ExpiryDate = new DateTime(2022, 4, 7), StatusId = 2, ManagerId = 2, ProjectId = 3 }
            };

        private static IEnumerable<User> ExpectedUsers =>
            new[]
            {
                new User { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new User { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new User { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" }
            };

        private static IEnumerable<UserProject> ExpectedUserProjects =>
            new[]
            {
                new UserProject { Id = 1, PositionId = 1, TaskId = 1, UserId = 1 },
                new UserProject { Id = 2, PositionId = 2, TaskId = 2, UserId = 1 },
                new UserProject { Id = 3, PositionId = 3, TaskId = 3, UserId = 1 },
                new UserProject { Id = 4, PositionId = 2, TaskId = 4, UserId = 1 },
                new UserProject { Id = 5, PositionId = 1, TaskId = 3, UserId = 1 }
            };

        private static IEnumerable<Position> ExpectedPositions =>
            new[]
            {
                new Position { Id = 1, Name = "Name1", Description = "Description1" },
                new Position { Id = 2, Name = "Name2", Description = "Description2" },
                new Position { Id = 3, Name = "Name3", Description = "Description3" }
            };

        private static IEnumerable<ProjectStatus> ExpectedProjectStatuses =>
            new[]
            {
                new ProjectStatus { Id = 1, Name = "Status1" },
                new ProjectStatus { Id = 2, Name = "Status2" },
                new ProjectStatus { Id = 3, Name = "Status3" }
            };
    }
}
