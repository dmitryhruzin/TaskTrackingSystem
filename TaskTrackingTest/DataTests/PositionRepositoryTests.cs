using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.DataTests
{
    [TestFixture]
    public class PositionRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task PositionRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var positionRepository = new PositionRepository(context);

            var expected = ExpectedPositions.FirstOrDefault(x => x.Id == id);

            //Act
            var actual = await positionRepository.GetByIdAsync(id);

            //Assert
            Assert.That(actual, Is.EqualTo(expected).Using(new PositionEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task PositionRepository_GetAllAsync_ReturnsAllValues()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var positionRepository = new PositionRepository(context);

            //Act
            var actual = await positionRepository.GetAllAsync();

            //Assert
            Assert.That(actual, Is.EqualTo(ExpectedPositions).Using(new PositionEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PositionRepository_AddAsync_AddsValueToDatabase()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var positionRepository = new PositionRepository(context);

            var position = new Position { Id = 4, Name = "Status4", Description = "Description4" };

            //Act
            await positionRepository.AddAsync(position);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Positions.Count(), Is.EqualTo(4), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PositionRepository_DeleteByIdAsync_DeletesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var positionRepository = new PositionRepository(context);

            //Act
            await positionRepository.DeleteByIdAsync(1);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(context.Positions.Count(), Is.EqualTo(2), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task PositionRepository_Update_UpdatesEntity()
        {
            //Arrange
            using var context = new TaskDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var positionRepository = new PositionRepository(context);

            var position = new Position
            {
                Id = 1,
                Name = "Update name",
                Description = "Update description"
            };

            //Act
            positionRepository.Update(position);

            await context.SaveChangesAsync();

            //Assert
            Assert.That(position, Is.EqualTo(new Position
            {
                Id = 1,
                Name = "Update name",
                Description = "Update description"
            }).Using(new PositionEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<Position> ExpectedPositions =>
            new[]
            {
                new Position { Id = 1, Name = "Name1", Description = "Description1" },
                new Position { Id = 2, Name = "Name2", Description = "Description2" },
                new Position { Id = 3, Name = "Name3", Description = "Description3" }
            };
    }
}
