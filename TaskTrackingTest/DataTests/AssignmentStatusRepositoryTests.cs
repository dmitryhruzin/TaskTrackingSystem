using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.DataTests
{
    [TestFixture]
    public class AssignmentStatusRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task AssignmentStatusRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentStatusRepository = new AssignmentStatusRepository(context);

            var expected = ExpectedAssignmentStatuses.FirstOrDefault(x => x.Id == id);

            //Act
            var actual = await assignmentStatusRepository.GetByIdAsync(id);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new AssignmentStatusEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentStatusRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentStatusRepository = new AssignmentStatusRepository(context);

            //Act
            var actual = await assignmentStatusRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(ExpectedAssignmentStatuses).Using(new AssignmentStatusEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentStatusRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentStatusRepository = new AssignmentStatusRepository(context);

            var assignmentStatus = new AssignmentStatus { Id = 4, Name = "Status4" };

            //Act
            await assignmentStatusRepository.AddAsync(assignmentStatus);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.AssignmentStatuses.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task AssignmentStatusRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentStatusRepository = new AssignmentStatusRepository(context);

            //Act
            await assignmentStatusRepository.DeleteByIdAsync(1);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.AssignmentStatuses.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task AssignmentStatusRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var assignmentStatusRepository = new AssignmentStatusRepository(context);

            var assignmentStatus = new AssignmentStatus
            {
                Id = 1,
                Name = "Finished"
            };

            //Act
            assignmentStatusRepository.Update(assignmentStatus);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(assignmentStatus, Is.EqualTo(new AssignmentStatus
            {
                Id = 1,
                Name = "Finished"
            }).Using(new AssignmentStatusEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<AssignmentStatus> ExpectedAssignmentStatuses =>
            new[]
            {
                new AssignmentStatus { Id = 1, Name = "Status1" },
                new AssignmentStatus { Id = 2, Name = "Status2" },
                new AssignmentStatus { Id = 3, Name = "Status3" }
            };
    }
}
