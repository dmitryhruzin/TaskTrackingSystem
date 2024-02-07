using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.IntegrationTests
{
    public class UserIntegrationTest
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/users/";

        [SetUp]
        public void Init()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllUserModels()
        {
            // Arrange
            var expected = UserModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllRoleModels()
        {
            // Arrange
            var expected = RoleModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "roles");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<RoleModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersController_GetById_ReturnsUserModel()
        {
            //arrange
            var expected = UserModels.First();

            //act
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<UserModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected, optons =>
                optons.Excluding(t => t.UserProjectIds).Excluding(t => t.TaskIds));
        }

        [Test]
        public async Task UsersController_GetByIdAsync_ReturnsNotFound()
        {
            //arrange
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + id);

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UsersController_GetById_ReturnsRoleModel()
        {
            //arrange
            var expected = RoleModels.First();

            //act
            var httpResponse = await _client.GetAsync(RequestUri + $"roles/{1}");

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<RoleModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersController_GetByIdAsync_ReturnsNotFoundRole()
        {
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + $"roles/{id}");

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UsersController_Add_AddsUserToDb()
        {
            //arrange
            var user = new RegisterUserModel
            {
                FirstName = "Name4",
                LastName = "Lastname4",
                UserName = "Username4",
                Email = "email4@gmail.com",
                Password = ".1aApassword",
                ConfirmPassword = ".1aApassword"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Users.Should().HaveCount(4);
            }

        }

        [Test]
        public async Task UsersController_Add_ThrowsExceptionIfModelIsInvalid()
        {
            //arrange
            var user = new RegisterUserModel
            {
                FirstName = "",
                LastName = "Lastname4",
                UserName = "Username4",
                Email = "email4@gmail.com",
                Password = ".1aApassword",
                ConfirmPassword = ".1aApassword"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UsersController_Add_AddsRoleToDb()
        {
            //arrange
            var role = new RoleModel
            {
                Id = 4,
                Name = "Name4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "roles", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Roles.Should().HaveCount(4);
            }
        }

        [Test]
        public async Task UsersController_Add_ThrowsExceptionIfNameIsInvalid()
        {
            //arrange
            var role = new RoleModel
            {
                Id = 4,
                Name = ""
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "roles", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UsersController_Update_UpdatesRoleInDb()
        {
            //arrange
            var role = new RoleModel
            {
                Id = 1,
                Name = "Name4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "roles", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                if (context != null)
                {
                    context.Roles.Should().HaveCount(3);

                    var DbModel = await context.Roles.FindAsync(1);
                    DbModel.Should().NotBeNull().And.BeEquivalentTo(role, options =>
                        options.Excluding(x => x.Id).ExcludingMissingMembers());
                }
            }
        }

        [Test]
        public async Task UsersController_Update_ThrowsExceptionIfStatusIsInvalid()
        {
            //arrange
            var role = new RoleModel
            {
                Id = 1,
                Name = ""
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "roles", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UsersController_Delete_DeletesUserFromDb()
        {
            // arrange
            var id = 1;

            // act
            var httpResponse = await _client.DeleteAsync(RequestUri + id);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Users.Should().HaveCount(2);
            }
        }

        [Test]
        public async Task UsersController_Delete_DeletesStatusFromDb()
        {
            // arrange
            var id = 1;

            // act
            var httpResponse = await _client.DeleteAsync(RequestUri + $"roles/{id}");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Roles.Should().HaveCount(2);
            }
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllTasksByUserIdModels()
        {
            // Arrange
            var expected = TaskModels;

            var id = 1;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{id}/tasks");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllTasksByManagerIdModels()
        {
            // Arrange
            var expected = TaskModels;

            var id = 2;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{id}/manager/tasks");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllProjectsByUserIdModels()
        {
            // Arrange
            var expected = ProjectModels;

            var id = 1;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{id}/projects");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<ProjectModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UsersContoller_GetAll_ReturnsAllPositionsByUserIdModels()
        {
            // Arrange
            var expected = PositionModels;

            var id = 1;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{id}/positions");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<PositionModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        private readonly IEnumerable<UserModel> UserModels =
            new List<UserModel>
            {
                new UserModel 
                { 
                    Id = 1, 
                    FirstName = "Firstname1", 
                    LastName = "Lastname1", 
                    UserName = "Username1", 
                    Email = "email1@gmail.com" ,
                    TaskIds = new List<int>(),
                    UserProjectIds = new List<int>{1,2,3,4,5}
                },
                new UserModel 
                { 
                    Id = 2, 
                    FirstName = "Firstname2", 
                    LastName = "Lastname2", 
                    UserName = "Username2", 
                    Email = "email2@gmail.com",
                    TaskIds = new List<int>{ 1,2,3,4},
                    UserProjectIds = new List<int>()
                },
                new UserModel 
                { 
                    Id = 3, 
                    FirstName = "Firstname3", 
                    LastName = "Lastname3", 
                    UserName = "Username3", 
                    Email = "email3@gmail.com",
                    TaskIds = new List<int>(),
                    UserProjectIds = new List<int>()
                }
            };

        private readonly IEnumerable<RoleModel> RoleModels =
            new List<RoleModel>
            {
                new RoleModel
                {
                    Id = 1,
                    Name = "User"
                },
                new RoleModel
                {
                    Id = 2,
                    Name = "Manager"
                },
                new RoleModel
                {
                    Id = 3,
                    Name = "Administrator"
                }
            };

        private readonly IEnumerable<TaskModel> TaskModels =
            new List<TaskModel>
            {
                new TaskModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7),
                    StatusId = 1, StatusName= "Status1",
                    ManagerId=2, ManagerUserName="Username2",
                    ProjectId=1, ProjectName="Name1",
                    UserProjectIds=new List<int>(){1}
                },
                new TaskModel { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, StatusName= "Status2",
                    ManagerId=2, ManagerUserName="Username2",
                    ProjectId=2, ProjectName="Name2",
                    UserProjectIds=new List<int>(){2}
                },
                new TaskModel { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, StatusName= "Status3",
                    ManagerId=2, ManagerUserName="Username2",
                    ProjectId=3, ProjectName="Name3",
                    UserProjectIds=new List<int>(){3, 5}
                },
                new TaskModel { Id = 4, Name = "Name4", Description = "Description4", StartDate = new DateTime(2022, 4, 1), ExpiryDate = new DateTime(2022, 4, 7),
                    StatusId = 2, StatusName= "Status2",
                    ManagerId=2, ManagerUserName="Username2",
                    ProjectId=3, ProjectName="Name3",
                    UserProjectIds = new List<int>(){4}
                }
            };

        private readonly IEnumerable<ProjectModel> ProjectModels =
            new List<ProjectModel>
            {
                new ProjectModel { Id = 1, Name = "Name1", Description = "Description1", StartDate = new DateTime(2022, 1, 1), ExpiryDate = new DateTime(2022, 1, 7),
                    StatusId = 1, StatusName= "Status1",
                    TaskIds= new List<int>(){1}
                },
                new ProjectModel { Id = 2, Name = "Name2", Description = "Description2", StartDate = new DateTime(2022, 2, 1), ExpiryDate = new DateTime(2022, 2, 7),
                    StatusId = 2, StatusName= "Status2",
                    TaskIds= new List<int>(){2}
                },
                new ProjectModel { Id = 3, Name = "Name3", Description = "Description3", StartDate = new DateTime(2022, 3, 1), ExpiryDate = new DateTime(2022, 3, 7),
                    StatusId = 3, StatusName= "Status3",
                    TaskIds= new List<int>(){3, 4}
                }
            };

        private readonly IEnumerable<PositionModel> PositionModels =
            new List<PositionModel>
            {
                new PositionModel { Id = 1, Name = "Name1", Description = "Description1" },
                new PositionModel { Id = 2, Name = "Name2", Description = "Description2" },
                new PositionModel { Id = 3, Name = "Name3", Description = "Description3" }
            };
    }
}
