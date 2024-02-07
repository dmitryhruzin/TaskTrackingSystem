using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.DataTests
{
    [TestFixture]
    public class AssignmentRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task AssignmentRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            var expected = ExpectedTasks.FirstOrDefault(x => x.Id == id);

            //Act
            var actual = await assignmentRepository.GetByIdAsync(id);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new AssignmentEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            //Act
            var actual = await assignmentRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(ExpectedTasks).Using(new AssignmentEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            var assignment = new Assignment { Id = 5, Name = "Name5", Description = "Description5", StartDate = DateTime.Now.AddDays(-7), ExpiryDate = DateTime.Now.AddDays(7), StatusId = 1, ManagerId = 2, ProjectId = 1 };

            //Act
            await assignmentRepository.AddAsync(assignment);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Assignments.Count(), Is.EqualTo(5), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            //Act
            await assignmentRepository.DeleteByIdAsync(1);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Assignments.Count(), Is.EqualTo(3), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task AssignmentRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            var assignment = new Assignment
            {
                Id = 1,
                Name = "Update name",
                Description = "Update description",
                StartDate = DateTime.Now.AddDays(-7),
                ExpiryDate = DateTime.Now.AddDays(5),
                StatusId = 2,
                ManagerId = 2,
                ProjectId = 2
            };

            //Act
            assignmentRepository.Update(assignment);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(assignment, Is.EqualTo(new Assignment
            {
                Id = 1,
                Name = "Update name",
                Description = "Update description",
                StartDate = DateTime.Now.AddDays(-7),
                ExpiryDate = DateTime.Now.AddDays(5),
                StatusId = 2,
                ManagerId = 2,
                ProjectId = 2
            }).Using(new AssignmentEqualityComparer()), message: "Update method works incorrect");
        }

        [Test]
        public async Task AssignmentRepository_GetByIdWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            var expected = ExpectedTasks.FirstOrDefault(x => x.Id == 1);

            //Act
            var actual = await assignmentRepository.GetByIdWithDetailsAsync(1);

            //Assert
            Assert.That(actual,
                Is.EqualTo(expected).Using(new AssignmentEqualityComparer()), message: "GetByIdWithDetailsAsync method works incorrect");

            Assert.That(actual.Project,
                Is.EqualTo(ExpectedProjects.FirstOrDefault(i => i.Id == expected.ProjectId)).Using(new ProjectEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Manager,
                Is.EqualTo(ExpectedUsers.FirstOrDefault(i => i.Id == 2)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.UserProjects.OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUserProjects.Where(i => i.Id == 1)).Using(new UserProjectEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.UserProjects.Select(j => j.User).OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers.Where(i => i.Id == 1)).Using(new UserEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Status,
                Is.EqualTo(ExpectedAssignmentStatuses.FirstOrDefault(i => i.Id == expected.StatusId)).Using(new AssignmentStatusEqualityComparer()), message: "GetByIdWithDetailsAsync method doesnt't return included entities");
        }

        [Test]
        public async Task AssignmentRepository_GetAllWithDetailsAsync_ReturnsWithIncludedEntities()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentRepository = new AssignmentRepository(context);

            //Act
            var actual = await assignmentRepository.GetAllWithDetailsAsync();

            //Assert
            Assert.That(actual,
                Is.EqualTo(ExpectedTasks).Using(new AssignmentEqualityComparer()), message: "GetAllWithDetailsAsync method works incorrect");

            Assert.That(actual.Select(i => i.Project).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedProjects).Using(new ProjectEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Manager).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers.Where(i => i.Id == 2)).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.SelectMany(i => i.UserProjects).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUserProjects).Using(new UserProjectEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.SelectMany(i => i.UserProjects.Select(j => j.User)).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedUsers.Where(i => i.Id == 1)).Using(new UserEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");

            Assert.That(actual.Select(i => i.Status).Distinct().OrderBy(i => i.Id),
                Is.EqualTo(ExpectedAssignmentStatuses).Using(new AssignmentStatusEqualityComparer()), message: "GetAllWithDetailsAsync method doesnt't return included entities");
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
    }
}
