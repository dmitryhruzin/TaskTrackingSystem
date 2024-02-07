using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.BusinessTests
{
    public class ProjectServiceTests
    {
        [Test]
        public async Task ProjectService_GetAll_ReturnsAllProjects()
        {
            //Arrange
            var expected = ProjectModels.ToList();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.ProjectRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(ProjectEntities.AsEnumerable());

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetAllAsync();

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.TaskIds));
        }

        [Test]
        public async Task ProjectService_GetAllStatusesAsync_ReturnsAllStatuses()
        {
            //Arrange
            var expected = StatusModels;

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.ProjectStatusRepository.GetAllAsync())
                .ReturnsAsync(ProjectStatusEntities.AsEnumerable());

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetAllStatusesAsync();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ProjectService_GetById_ReturnsProjectModel(int id)
        {
            //Arrange
            var expected = ProjectModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ProjectRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(ProjectEntities.FirstOrDefault(x => x.Id == id));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetByIdAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
              options.Excluding(x => x.TaskIds));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ProjectService_GetById_ReturnsStatusModel(int id)
        {
            //Arrange
            var expected = StatusModels.FirstOrDefault(x => x.Id == id);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ProjectStatusRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProjectStatusEntities.FirstOrDefault(x => x.Id == id));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetStatusByIdAsync(id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ProjectService_AddAsync_AddsProject()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.AddAsync(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, StatusName = "Status1" };

            //Act
            await projectService.AddAsync(project);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectRepository.AddAsync(It.Is<Project>(c => c.Id == project.Id && c.StatusId == project.StatusId && c.Name == project.Name && c.Description == project.Description && c.StartDate == project.StartDate && c.ExpiryDate == project.ExpiryDate)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ProjectService_AddAsync_AddsStatus()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectStatusRepository.AddAsync(It.IsAny<ProjectStatus>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var status = new StatusModel { Id = 1, Name = "Status1" };

            //Act
            await projectService.AddStatusAsync(status);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectStatusRepository.AddAsync(It.Is<ProjectStatus>(c => c.Id == status.Id && c.Name == status.Name)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ProjectService_AddAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.AddAsync(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = string.Empty, Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.AddAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_AddAsync_ThrowsTaskTrackingExceptionWithEmptyDescription()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.AddAsync(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = "Name1", Description = string.Empty, StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.AddAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_AddAsync_ThrowsTaskTrackingExceptionWithIncorrectExpireDate()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.AddAsync(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 7), ExpiryDate = new DateTime(2022, 1, 1), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.AddAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_AddStatusAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectStatusRepository.AddAsync(It.IsAny<ProjectStatus>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var status = new StatusModel { Id = 1, Name = string.Empty };

            //Act
            Func<Task> act = async () => await projectService.AddStatusAsync(status);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ProjectService_DeleteAsync_DeletesProject(int id)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.DeleteByIdAsync(It.IsAny<int>()));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));
            mockUnitOfWork
                .Setup(x => x.ProjectRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(ProjectEntityWidthTask);
            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);

            //Act
            await projectService.DeleteAsync(id);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ProjectService_DeleteAsync_DeletesStatus(int id)
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectStatusRepository.DeleteByIdAsync(It.IsAny<int>()));
            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            await projectService.DeleteStatusAsync(id);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectStatusRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ProjectService_UpdateAsync_UpdatesProject()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.Update(It.IsAny<Project>()));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));
            mockUnitOfWork
                .Setup(x => x.ProjectRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(ProjectEntityWidthTask);

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var project = new ProjectModel { Id = 4, Name = "Updated Name", Description = "Updated Description", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 9), StatusId = 2, StatusName = "Status2" };

            //Act
            await projectService.UpdateAsync(project);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectRepository.Update(It.Is<Project>(c => c.Id == project.Id && c.StatusId == project.StatusId && c.Name == project.Name && c.Description == project.Description && c.StartDate == project.StartDate && c.ExpiryDate == project.ExpiryDate)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ProjectService_UpdateAsync_UpdatesStatus()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectStatusRepository.Update(It.IsAny<ProjectStatus>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var status = new StatusModel { Id = 4, Name = "Updated Name" };

            //Act
            await projectService.UpdateStatusAsync(status);

            //Assert
            mockUnitOfWork.Verify(x => x.ProjectStatusRepository.Update(It.Is<ProjectStatus>(c => c.Id == status.Id && c.Name == status.Name)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task ProjectService_UpdateAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.Update(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = string.Empty, Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.UpdateAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_UpdateAsync_ThrowsTaskTrackingExceptionWithEmptyDescription()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.Update(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = "Name1", Description = string.Empty, StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.UpdateAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_UpdateAsync_ThrowsTaskTrackingExceptionWithIncorrectExpireDate()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectRepository.Update(It.IsAny<Project>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var project = new ProjectModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 7), ExpiryDate = new DateTime(2022, 1, 1), StatusId = 1, StatusName = "Status1" };

            //Act
            Func<Task> act = async () => await projectService.UpdateAsync(project);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_UpdateStatusAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ProjectStatusRepository.Update(It.IsAny<ProjectStatus>()));

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);
            var status = new StatusModel { Id = 1, Name = string.Empty };

            //Act
            Func<Task> act = async () => await projectService.UpdateStatusAsync(status);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task ProjectService_GetAll_ReturnsAllTasksByProjectId()
        {
            //Arrange
            var expected = TaskModels.ToList();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.AssignmentRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(TaskEntities.AsEnumerable());

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetAllTasksByProjectIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.UserProjectIds));
        }

        [Test]
        public async Task ProjectService_GetAll_ReturnsAllUsersByProjectId()
        {
            //Arrange
            var expected = UserModels.Where(t => t.Id == 1).ToList();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.ProjectRepository.GetByIdWithDetailsAsync(It.IsAny<int>()))
                .ReturnsAsync(ProjectEntityWidthTask);

            var projectService = new ProjectService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await projectService.GetAllUsersByProjectIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
              options.Excluding(x => x.TaskIds).Excluding(x=>x.UserProjectIds));
        }

        private static IEnumerable<ProjectModel> ProjectModels =>
            new List<ProjectModel>
            {
                new ProjectModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), 
                    StatusId = 1, StatusName= "Status1"},
                new ProjectModel { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), 
                    StatusId = 2, StatusName= "Status2" },
                new ProjectModel { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), 
                    StatusId = 3, StatusName= "Status3" }
            };

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

        private static Project ProjectEntityWidthTask =>
            new Project
            {
                Id = 1,
                Name = "Name1",
                Description = "Description1",
                StartDate = new DateTime(2022, 1, 1),
                ExpiryDate = new DateTime(2022, 1, 7),
                StatusId = 1,
                Status = ProjectStatusEntities.ElementAt(0),
                Tasks = TaskEntities.ToList()
            };

        private static IEnumerable<TaskModel> TaskModels =>
            new List<TaskModel>
            {
                new TaskModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), 
                    StatusId = 1, StatusName= "Status1", 
                    ManagerId=2, ManagerUserName="Username2", 
                    ProjectId=1, ProjectName="Name1",},
                new TaskModel { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7), 
                    StatusId = 2, StatusName= "Status2", 
                    ManagerId=2, ManagerUserName="Username2", 
                    ProjectId=1, ProjectName="Name1" },
                new TaskModel { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), 
                    StatusId = 3, StatusName= "Status3", 
                    ManagerId=2, ManagerUserName="Username2", 
                    ProjectId=1, ProjectName="Name1" }
            };

        private static IEnumerable<Assignment> TaskEntities =>
           new List<Assignment>
           {
                new Assignment { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7), 
                    StatusId = 1, Status = TaskStatusEntities.ElementAt(0), 
                    ManagerId = 2, Manager = UserEntities.ElementAt(1), 
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0),
                    UserProjects = UserProjectEntities.Where(t=>t.TaskId==1).ToList()},
                new Assignment { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, Status = TaskStatusEntities.ElementAt(1), 
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0),
                    UserProjects = UserProjectEntities.Where(t=>t.TaskId==2).ToList() },
               new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7), 
                    StatusId = 3, Status = TaskStatusEntities.ElementAt(2), 
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0),
                    UserProjects = UserProjectEntities.Where(t=>t.TaskId==3).ToList() }
           };

        private static IEnumerable<StatusModel> StatusModels =>
            new List<StatusModel>
            {
                new StatusModel { Id = 1, Name = "Status1" },
                new StatusModel { Id = 2, Name = "Status2" },
                new StatusModel { Id = 3, Name = "Status3" }
            };

        private static IEnumerable<UserProject> UserProjectEntities =>
            new List<UserProject>
            {
                new UserProject { Id = 1, 
                    PositionId = 1, 
                    TaskId = 1,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 2, 
                    PositionId = 2, 
                    TaskId = 2,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 3, 
                    PositionId = 3, 
                    TaskId = 3,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 4, 
                    PositionId = 2, 
                    TaskId = 3,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 5, 
                    PositionId = 1, 
                    TaskId = 2,
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

        private static IEnumerable<User> UserEntities =>
            new List<User>
            {
                new User { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new User { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new User { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" }
            };

        private static IEnumerable<UserModel> UserModels =>
            new List<UserModel>
            {
                new UserModel { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new UserModel { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new UserModel { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" }
            };
    }
}
