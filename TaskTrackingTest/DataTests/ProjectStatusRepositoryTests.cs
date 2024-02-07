using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.DataTests
{
    [TestFixture]
    public class ProjectStatusRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ProjectStatusRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var projectStatusRepository = new ProjectStatusRepository(context);

            var expected = ExpectedProjectStatuses.FirstOrDefault(x => x.Id == id);

            //Act
            var actual = await projectStatusRepository.GetByIdAsync(id);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new ProjectStatusEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task ProjectStatusRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var projectStatusRepository = new ProjectStatusRepository(context);

            //Act
            var actual = await projectStatusRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(ExpectedProjectStatuses).Using(new ProjectStatusEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task ProjectStatusRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var projectStatusRepository = new ProjectStatusRepository(context);

            var projectStatus = new ProjectStatus { Id = 4, Name = "Status4" };

            //Act
            await projectStatusRepository.AddAsync(projectStatus);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.ProjectStatuses.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ProjectStatusRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var projectStatusRepository = new ProjectStatusRepository(context);

            //Act
            await projectStatusRepository.DeleteByIdAsync(1);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.ProjectStatuses.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task ProjectStatusRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var projectStatusRepository = new ProjectStatusRepository(context);

            var projectStatus = new ProjectStatus
            {
                Id = 1,
                Name = "Finished"
            };

            //Act
            projectStatusRepository.Update(projectStatus);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(projectStatus, Is.EqualTo(new ProjectStatus
            {
                Id = 1,
                Name = "Finished"
            }).Using(new ProjectStatusEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<ProjectStatus> ExpectedProjectStatuses =>
            new[]
            {
                new ProjectStatus { Id = 1, Name = "Status1" },
                new ProjectStatus { Id = 2, Name = "Status2" },
                new ProjectStatus { Id = 3, Name = "Status3" }
            };
    }
}
