using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.BusinessTests
{
    public class UserProjectServiceTests
    {
        [Test]
        public async Task UserProjectService_GetAll_ReturnsAllUserProjects()
        {
            //Arrange
            var expected = UserProjectModels.ToList();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.UserProjectRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(UserProjectEntities.AsEnumerable());

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userProjectService.GetAllAsync();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectService_GetAllPositionsAsync_ReturnsAllPositions()
        {
            //Arrange
            var expected = PositionModels;

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.PositionRepository.GetAllAsync())
                .ReturnsAsync(PositionEntities.AsEnumerable());

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userProjectService.GetAllPositionsAsync();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task UserProjectService_GetById_ReturnsUserProjectModel(int id)
        {
            //Arrange
            var expected = UserProjectModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.UserProjectRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UserProjectEntities.FirstOrDefault(x => x.Id == id));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userProjectService.GetByIdAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UserProjectService_GetById_ReturnsPositionModel(int id)
        {
            //Arrange
            var expected = PositionModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.PositionRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(PositionEntities.FirstOrDefault(x => x.Id == id));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userProjectService.GetPositionByIdAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectService_AddAsync_AddsUserProject()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserProjectRepository.AddAsync(It.IsAny<UserProject>()));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var userProject = new UserProjectModel
            {
                Id = 1,
                PositionId = 1,
                PositionName = "Name1",
                TaskId = 1,
                TaskName = "Name1",
                UserId = 1,
                UserName = "UserName1",
                UserEmail = "UserEmail"
            };

            //Act
            await userProjectService.AddAsync(userProject);

            //Assert
            mockUnitOfWork.Verify(x => x.UserProjectRepository.AddAsync(It.Is<UserProject>(c => c.Id == userProject.Id && c.TaskId == userProject.TaskId && c.UserId == userProject.UserId && c.PositionId == userProject.PositionId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_AddAsync_AddsPosition()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.AddAsync(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = "Name1", Description  = "Description" };

            //Act
            await userProjectService.AddPositionAsync(position);

            //Assert
            mockUnitOfWork.Verify(x => x.PositionRepository.AddAsync(It.Is<Position>(c => c.Id == position.Id && c.Name == position.Name && c.Description == position.Description)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_AddPositionAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.AddAsync(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = string.Empty, Description = "Description" };

            //Act
            Func<Task> act = async () => await userProjectService.AddPositionAsync(position);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserProjectService_AddPositionAsync_ThrowsTaskTrackingExceptionWithEmptyDescription()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.AddAsync(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = "Name1", Description = string.Empty };

            //Act
            Func<Task> act = async () => await userProjectService.AddPositionAsync(position);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task UserProjectService_DeleteAsync_DeletesUserProject(int id)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserProjectRepository.DeleteByIdAsync(It.IsAny<int>()));
            mockUnitOfWork
                .Setup(x => x.UserProjectRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(UserProjectEntities.FirstOrDefault(x => x.Id == id));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));
            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);

            //Act
            await userProjectService.DeleteAsync(id);

            //Assert
            mockUnitOfWork.Verify(x => x.UserProjectRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UserProjectService_DeleteAsync_DeletesPosition(int id)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.DeleteByIdAsync(It.IsAny<int>()));
            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            await userProjectService.DeletePositionAsync(id);

            //Assert
            mockUnitOfWork.Verify(x => x.PositionRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_UpdateAsync_UpdatesUserProject()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserProjectRepository.Update(It.IsAny<UserProject>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var userProject = new UserProjectModel
            {
                Id = 1,
                PositionId = 1,
                PositionName = "Name1",
                TaskId = 1,
                TaskName = "Name1",
                UserId = 1,
                UserName = "UserName1"
            };

            //Act
            await userProjectService.UpdateAsync(userProject);

            //Assert
            mockUnitOfWork.Verify(x => x.UserProjectRepository.Update(It.Is<UserProject>(c => c.Id == userProject.Id && c.TaskId == userProject.TaskId && c.UserId == userProject.UserId && c.PositionId == userProject.PositionId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_UpdateAsync_UpdatesPosition()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.Update(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = "Name1", Description = "Description" };

            //Act
            await userProjectService.UpdatePositionAsync(position);

            //Assert
            mockUnitOfWork.Verify(x => x.PositionRepository.Update(It.Is<Position>(c => c.Id == position.Id && c.Name == position.Name && c.Description == position.Description)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_UpdatePositionAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.Update(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = string.Empty, Description = "Description" };

            //Act
            Func<Task> act = async () => await userProjectService.UpdatePositionAsync(position);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserProjectService_UpdatePositionAsync_ThrowsTaskTrackingExceptionWithEmptyDescription()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PositionRepository.Update(It.IsAny<Position>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var position = new PositionModel { Id = 1, Name = "Name1", Description = string.Empty };

            //Act
            Func<Task> act = async () => await userProjectService.UpdatePositionAsync(position);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserProjectService_AddAsync_AddsUserProjects()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserProjectRepository.AddAsync(It.IsAny<UserProject>()));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var userProjects = UserProjectModels.ToList();

            //Act
            await userProjectService.AddUserProjectsAsync(userProjects);

            //Assert
            mockUnitOfWork.Verify(x => x.UserProjectRepository.AddAsync(It.IsAny<UserProject>()), Times.Exactly(userProjects.Count));
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserProjectService_DeleteAsync_DeletesUserProjects()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserProjectRepository.DeleteByIdAsync(It.IsAny<int>()));
            var userProjectService = new UserProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var userProjectIds = UserProjectModels.Select(t => t.Id).ToList();

            //Act
            await userProjectService.DeleteUserProjectsAsync(userProjectIds);

            //Assert
            mockUnitOfWork.Verify(x => x.UserProjectRepository.DeleteByIdAsync(It.IsAny<int>()), Times.Exactly(userProjectIds.Count));
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        private static IEnumerable<Project> ProjectEntities =>
           new List<Project>
           {
                new Project { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7),
                    StatusId = 1, Status = ProjectStatusEntities.ElementAt(0)},
                new Project { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, Status = ProjectStatusEntities.ElementAt(1)},
                new Project { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3 ,
                    Status = ProjectStatusEntities.ElementAt(2)}
           };

        private static IEnumerable<Assignment> TaskEntities =>
           new List<Assignment>
           {
                new Assignment { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7),
                    StatusId = 1, Status = TaskStatusEntities.ElementAt(0),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0)},
                new Assignment { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, Status = TaskStatusEntities.ElementAt(1),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0) },
               new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, Status = TaskStatusEntities.ElementAt(2),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0) }
           };

        private static IEnumerable<UserProjectModel> UserProjectModels =>
            new List<UserProjectModel>
            {
                new UserProjectModel { Id = 1,
                    PositionId = 1, PositionName = "Name1",
                    TaskId = 1, TaskName="Name1",
                    UserId = 1, UserName="Username1", UserEmail="email1@gmail.com" },
                new UserProjectModel { Id = 2,
                    PositionId = 2,PositionName = "Name2",
                    TaskId = 2, TaskName="Name2",
                    UserId = 1, UserName="Username1", UserEmail="email1@gmail.com" },
                new UserProjectModel { Id = 3,
                    PositionId = 3,PositionName = "Name3",
                    TaskId = 3, TaskName="Name3",
                    UserId = 1, UserName="Username1", UserEmail="email1@gmail.com" },
                new UserProjectModel { Id = 4,
                    PositionId = 2,PositionName = "Name2",
                    TaskId = 3,TaskName="Name3",
                    UserId = 1, UserName="Username1", UserEmail="email1@gmail.com" },
                new UserProjectModel { Id = 5,
                    PositionId = 1,PositionName = "Name1",
                    TaskId = 2,TaskName="Name2",
                    UserId = 1, UserName="Username1", UserEmail="email1@gmail.com" }
            };

        private static IEnumerable<UserProject> UserProjectEntities =>
            new List<UserProject>
            {
                new UserProject { Id = 1,
                    PositionId = 1, Position = PositionEntities.ElementAt(0),
                    TaskId = 1, Task = TaskEntities.ElementAt(0),
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 2,
                    PositionId = 2, Position = PositionEntities.ElementAt(1),
                    TaskId = 2,Task = TaskEntities.ElementAt(1),
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 3,
                    PositionId = 3, Position = PositionEntities.ElementAt(2),
                    TaskId = 3,Task = TaskEntities.ElementAt(2),
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 4,
                    PositionId = 2, Position = PositionEntities.ElementAt(1),
                    TaskId = 3,Task = TaskEntities.ElementAt(2),
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 5,
                    PositionId = 1, Position = PositionEntities.ElementAt(0),
                    TaskId = 2,Task = TaskEntities.ElementAt(1),
                    UserId = 1, User=UserEntities.ElementAt(0) }
            };

        private static IEnumerable<ProjectStatus> ProjectStatusEntities =>
            new List<ProjectStatus>
            {
                new ProjectStatus { Id = 1, Name = "Status1" },
                new ProjectStatus { Id = 2, Name = "Status2" },
                new ProjectStatus { Id = 3, Name = "Status3" }
            };

        private static IEnumerable<AssignmentStatus> TaskStatusEntities =>
            new List<AssignmentStatus>
            {
                new AssignmentStatus { Id = 1, Name = "Status1" },
                new AssignmentStatus { Id = 2, Name = "Status2" },
                new AssignmentStatus { Id = 3, Name = "Status3" }
            };

        private static IEnumerable<Position> PositionEntities =>
            new List<Position>
            {
                new Position { Id = 1, Name = "Name1", Description="Description1" },
                new Position { Id = 2, Name = "Name2", Description="Description1" },
                new Position { Id = 3, Name = "Name3", Description="Description1" }
            };

        private static IEnumerable<PositionModel> PositionModels =>
            new List<PositionModel>
            {
                new PositionModel { Id = 1, Name = "Name1", Description="Description1" },
                new PositionModel { Id = 2, Name = "Name2", Description="Description1" },
                new PositionModel { Id = 3, Name = "Name3", Description="Description1" }
            };

        private static IEnumerable<User> UserEntities =>
            new List<User>
            {
                new User { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new User { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new User { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" }
            };
    }
}
