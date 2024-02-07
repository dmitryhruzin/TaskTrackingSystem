using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.BusinessTests
{
    public class UserServiceTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UserService_GetById_ReturnsUser(int id)
        {
            // Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == id));

            var expected = UserModels.FirstOrDefault(t => t.Id == id);

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);

            // Act
            var actual = await userService.GetByIdAsync(id);

            // Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.TaskIds).Excluding(x=>x.UserProjectIds));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task UserService_GetById_ReturnsRole(int id)
        {
            // Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleEntities.FirstOrDefault(t => t.Id == id));

            var expected = RoleModels.FirstOrDefault(t => t.Id == id);

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);

            // Act
            var actual = await userService.GetRoleByIdAsync(id);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_AddAsync_AddsUser()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com",
                Password="*1aApassword",
                ConfirmPassword= "*1aApassword"
            };

            //Act
            await userService.AddAsync(user);

            //Assert
            fakeUserManager.Verify(x => x.CreateAsync(It.Is<User>(c => c.FirstName == user.FirstName && c.LastName == user.LastName && c.UserName == user.UserName && c.Email == user.Email), It.Is<string>(t => t == user.Password)), Times.Once);
            fakeUserManager.Verify(x => x.AddToRoleAsync(It.Is<User>(c => c.FirstName == user.FirstName && c.LastName == user.LastName && c.UserName == user.UserName && c.Email == user.Email), It.Is<string>(t => t == "User")), Times.Once);
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyFirstName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = string.Empty,
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com",
                Password = "*1aApassword",
                ConfirmPassword = "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyLastName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = string.Empty,
                UserName = "Username1",
                Email = "email1@gmail.com",
                Password = "*1aApassword",
                ConfirmPassword = "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyUserName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = string.Empty,
                Email = "email1@gmail.com",
                Password = "*1aApassword",
                ConfirmPassword = "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyEmail()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = string.Empty,
                Password = "*1aApassword",
                ConfirmPassword = "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyPassword()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com",
                Password = string.Empty,
                ConfirmPassword = "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddUserAsync_ThrowsTaskTrackingExceptionWithEmptyConfirmPassword()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()));
            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new RegisterUserModel
            {
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com",
                Password = "*1aApassword",
                ConfirmPassword = string.Empty
            };

            //Act
            Func<Task> act = async () => await userService.AddAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_AddAsync_AddsRole()
        {
            //Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(m => m.CreateAsync(It.IsAny<IdentityRole<int>>()));

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);
            var role = new RoleModel
            {
                Id = 1,
                Name="Name1"
            };

            //Act
            await userService.AddRoleAsync(role);

            //Assert
            mockRoleManager.Verify(x => x.CreateAsync(It.Is<IdentityRole<int>>(c => c.Name == role.Name && c.Id == role.Id)), Times.Once);
        }

        [Test]
        public async Task UserService_AddRoleAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(m => m.CreateAsync(It.IsAny<IdentityRole<int>>()));

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);
            var role = new RoleModel
            {
                Id = 1,
                Name = string.Empty
            };

            //Act
            Func<Task> act = async () => await userService.AddRoleAsync(role);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_UpdateAsync_UpdatesUser()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();


            fakeUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.First());
            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var user = new UpdateUserModel
            {
                Id=1,
                FirstName = "Firstname2",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com"
            };

            //Act
            await userService.UpdateAsync(user);

            //Assert
            fakeUserManager.Verify(x => x.UpdateAsync(It.Is<User>(c => c.FirstName == user.FirstName && c.LastName == user.LastName && c.UserName == user.UserName && c.Email == user.Email)), Times.Once);
        }

        [Test]
        public async Task UserService_UpdateRoleAsync_UpdatesRole()
        {
            //Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleEntities.First());
            mockRoleManager.Setup(m => m.UpdateAsync(It.IsAny<IdentityRole<int>>()));

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);
            var role = new RoleModel
            {
                Id = 1,
                Name = "Name2"
            };

            //Act
            await userService.UpdateRoleAsync(role);

            //Assert
            mockRoleManager.Verify(x => x.UpdateAsync(It.Is<IdentityRole<int>>(c => c.Name == role.Name && c.Id == role.Id)), Times.Once);
        }

        [Test]
        public async Task UserService_UpdateUserAsync_ThrowsTaskTrackingExceptionWithEmptyFirstName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.First());
            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new UpdateUserModel
            {
                Id=1,
                FirstName = string.Empty,
                LastName = "Lastname1",
                UserName = "Username1",
                Email = "email1@gmail.com"
            };

            //Act
            Func<Task> act = async () => await userService.UpdateAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_UpdateUserAsync_ThrowsTaskTrackingExceptionWithEmptyLastName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.First());
            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new UpdateUserModel
            {
                Id=1,
                FirstName = "Firstname1",
                LastName = string.Empty,
                UserName = "Username1",
                Email = "email1@gmail.com"
            };

            //Act
            Func<Task> act = async () => await userService.UpdateAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_UpdateUserAsync_ThrowsTaskTrackingExceptionWithEmptyUserName()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.First());
            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new UpdateUserModel
            {
                Id=1,
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = string.Empty,
                Email = "email1@gmail.com"
            };

            //Act
            Func<Task> act = async () => await userService.UpdateAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_UpdateUserAsync_ThrowsTaskTrackingExceptionWithEmptyEmail()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.First());
            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);
            var user = new UpdateUserModel
            {
                Id=1,
                FirstName = "Firstname1",
                LastName = "Lastname1",
                UserName = "Username1",
                Email = string.Empty
            };

            //Act
            Func<Task> act = async () => await userService.UpdateAsync(user);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_UpdateRoleAsync_ThrowsTaskTrackingExceptionWithEmptyName()
        {
            //Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(m => m.UpdateAsync(It.IsAny<IdentityRole<int>>()));
            mockRoleManager.Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleEntities.First());

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);
            var role = new RoleModel
            {
                Id = 1,
                Name = string.Empty
            };

            //Act
            Func<Task> act = async () => await userService.UpdateRoleAsync(role);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_DeleteAsync_DeletesUser()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager
                .Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t=>t.Id==1));

            fakeUserManager.Setup(m => m.DeleteAsync(It.IsAny<User>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);

            //Act
            await userService.DeleteAsync(1);

            //Assert
            fakeUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            fakeUserManager.Verify(x => x.DeleteAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task UserService_DeleteAsync_DeletesRole()
        {
            //Arrange
            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager
                .Setup(m => m.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleEntities.FirstOrDefault(t => t.Id == 1));

            mockRoleManager.Setup(m => m.DeleteAsync(It.IsAny<IdentityRole<int>>()));

            var userService = new UserService(mockRoleManager.Object, null, null, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            await userService.DeleteRoleAsync(1);

            //Assert
            mockRoleManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(x => x.DeleteAsync(It.IsAny<IdentityRole<int>>()), Times.Once);
        }

        [Test]
        public async Task UserService_AddAsync_AddsToRole()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == 1));

            fakeUserManager.Setup(t => t.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var role = new RoleModel
            {
                Id = 1,
                Name = "Name1"
            };

            //Act
            await userService.AddToRoleAsync(1, role);

            //Assert
            fakeUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            fakeUserManager.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UserService_AddAsync_AddsToRoles()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == 1));

            fakeUserManager.Setup(t => t.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var roles = RoleModels.ToList();

            //Act
            await userService.AddToRolesAsync(1, roles);

            //Assert
            fakeUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            fakeUserManager.Verify(x => x.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [Test]
        public async Task UserService_DeleteAsync_DeletesToRoles()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == 1));

            fakeUserManager.Setup(t => t.RemoveFromRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var roles = RoleModels.ToList();

            //Act
            await userService.DeleteToRolesAsync(1, roles.Select(t=>t.Name));

            //Assert
            fakeUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            fakeUserManager.Verify(x => x.RemoveFromRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Once);
        }

        [Test]
        public async Task UserService_DeleteAsync_DeletesToRole()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == 1));

            fakeUserManager.Setup(t => t.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(m => m.SendEmailAsync(It.IsAny<MessageModel>()));

            var userService = new UserService(null, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), mockEmailService.Object);
            var role = new RoleModel
            {
                Id = 1,
                Name = "Name1"
            };

            //Act
            await userService.DeleteToRoleAsync(1, role.Name);

            //Assert
            fakeUserManager.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            fakeUserManager.Verify(x => x.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnsAllPositionsByUserId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.UserProjectRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(UserProjectEntities.AsEnumerable());

            var expected = PositionModels.ToList();

            var userService = new UserService(null, null, mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userService.GetAllPositionsByUserIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnsAllProjectsByUserId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.UserProjectRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(UserProjectEntities.AsEnumerable());

            var expected = ProjectModels.ToList();

            var userService = new UserService(null, null, mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userService.GetAllProjectsByUserIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(t => t.TaskIds));
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnsAllTasksByUserId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.AssignmentRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(TaskWithUserProjectEntities.AsEnumerable());

            var expected = TaskModels.ToList();

            var userService = new UserService(null, null, mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userService.GetAllTasksByUserIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(t => t.UserProjectIds));
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnsAllTasksByManagerId()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.AssignmentRepository.GetAllWithDetailsAsync())
                .ReturnsAsync(TaskWithUserProjectEntities.AsEnumerable());

            var expected = TaskModels.ToList();

            var userService = new UserService(null, null, mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userService.GetAllTasksByManagerIdAsync(2);

            //Assert
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(t => t.UserProjectIds));
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnsAllRolesByUserId()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Id == 1));

            fakeUserManager.Setup(t => t.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RoleEntities.Where(t=>t.Name== "Name1").Select(t=>t.Name).ToList());

            var mockStore = new Mock<IRoleStore<IdentityRole<int>>>();

            var mockRoleManager = new Mock<RoleManager<IdentityRole<int>>>(mockStore.Object, null, null, null, null);

            mockRoleManager.Setup(t => t.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleEntities.FirstOrDefault(t => t.Name == "Name1"));

            var expected = RoleModels.Where(t => t.Name == "Name1");

            var userService = new UserService(mockRoleManager.Object, fakeUserManager.Object, null, UnitTestHelper.CreateMapperProfile(), null);

            //Act
            var actual = await userService.GetAllRolesByUserIdAsync(1);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

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

        private static IEnumerable<RoleModel> RoleModels =>
            new List<RoleModel>
            {
                new RoleModel{Id=1,Name="Name1"},
                new RoleModel{Id=2,Name="Name2"},
                new RoleModel{Id=3,Name="Name3"}
            };

        private static IEnumerable<IdentityRole<int>> RoleEntities =>
            new List<IdentityRole<int>>
            {
                new IdentityRole<int>{Id=1,Name="Name1",NormalizedName="NAME1"},
                new IdentityRole<int>{Id=2,Name="Name2",NormalizedName="NAME2"},
                new IdentityRole<int>{Id=3,Name="Name3",NormalizedName="NAME3"}
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

        private static IEnumerable<UserProjectModel> UserProjectModels =>
            new List<UserProjectModel>
            {
                new UserProjectModel { Id = 1,
                    PositionId = 1, PositionName = "Name1",
                    TaskId = 1, TaskName="Name1",
                    UserId = 1, UserName="Username1" },
                new UserProjectModel { Id = 2,
                    PositionId = 2,PositionName = "Name2",
                    TaskId = 2, TaskName="Name2",
                    UserId = 1, UserName="Username1" },
                new UserProjectModel { Id = 3,
                    PositionId = 3,PositionName = "Name3",
                    TaskId = 3, TaskName="Name3",
                    UserId = 1, UserName="Username1" },
                new UserProjectModel { Id = 4,
                    PositionId = 2,PositionName = "Name2",
                    TaskId = 3,TaskName="Name3",
                    UserId = 1, UserName="Username1" },
                new UserProjectModel { Id = 5,
                    PositionId = 1,PositionName = "Name1",
                    TaskId = 2,TaskName="Name2",
                    UserId = 1, UserName="Username1" }
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

        private static IEnumerable<UserProject> UserProjectWithoutTaskEntities =>
            new List<UserProject>
            {
                new UserProject { Id = 1,
                    PositionId = 1, Position = PositionEntities.ElementAt(0),
                    TaskId = 1, 
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 2,
                    PositionId = 2, Position = PositionEntities.ElementAt(1),
                    TaskId = 2,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 3,
                    PositionId = 3, Position = PositionEntities.ElementAt(2),
                    TaskId = 3,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 4,
                    PositionId = 2, Position = PositionEntities.ElementAt(1),
                    TaskId = 3,
                    UserId = 1, User=UserEntities.ElementAt(0) },
                new UserProject { Id = 5,
                    PositionId = 1, Position = PositionEntities.ElementAt(0),
                    TaskId = 2,
                    UserId = 1, User=UserEntities.ElementAt(0) }
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
                    ProjectId=1, ProjectName="Name2" },
                new TaskModel { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, StatusName= "Status3",
                    ManagerId=2, ManagerUserName="Username2",
                    ProjectId=1, ProjectName="Name3" }
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
                    ProjectId=1, Project =  ProjectEntities.ElementAt(1) },
               new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, Status = TaskStatusEntities.ElementAt(2),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(2) }
           };

        private static IEnumerable<Assignment> TaskWithUserProjectEntities =>
           new List<Assignment>
           {
                new Assignment { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7),
                    StatusId = 1, Status = TaskStatusEntities.ElementAt(0),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(0),
                    UserProjects = UserProjectWithoutTaskEntities.Where(t=>t.TaskId==1).ToList()
                },
                new Assignment { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, Status = TaskStatusEntities.ElementAt(1),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(1),
                    UserProjects = UserProjectWithoutTaskEntities.Where(t=>t.TaskId==2).ToList()
                },
               new Assignment { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, Status = TaskStatusEntities.ElementAt(2),
                    ManagerId = 2, Manager = UserEntities.ElementAt(1),
                    ProjectId=1, Project =  ProjectEntities.ElementAt(2),
                    UserProjects = UserProjectWithoutTaskEntities.Where(t=>t.TaskId==3).ToList()
               }
           };

        private static IEnumerable<AssignmentStatus> TaskStatusEntities =>
            new List<AssignmentStatus>
            {
                new AssignmentStatus { Id = 1, Name = "Status1" },
                new AssignmentStatus { Id = 2, Name = "Status2" },
                new AssignmentStatus { Id = 3, Name = "Status3" }
            };

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

        private static IEnumerable<ProjectStatus> ProjectStatusEntities =>
            new List<ProjectStatus>
            {
                new ProjectStatus { Id = 1, Name = "Status1" },
                new ProjectStatus { Id = 2, Name = "Status2" },
                new ProjectStatus { Id = 3, Name = "Status3" }
            };
    }
}
