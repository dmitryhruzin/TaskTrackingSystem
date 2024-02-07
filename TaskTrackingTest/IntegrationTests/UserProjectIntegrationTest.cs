using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.IntegrationTests
{
    public class UserProjectIntegrationTest
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/userprojects/";

        [SetUp]
        public void Init()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task UserProjectsContoller_GetAll_ReturnsAllUserProjectModels()
        {
            // Arrange
            var expected = UserProjectModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<UserProjectModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectsContoller_GetAll_ReturnsAllPositionModels()
        {
            // Arrange
            var expected = PositionModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "positions");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<PositionModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectsController_GetById_ReturnsUserProjectModel()
        {
            //arrange
            var expected = UserProjectModel;

            //act
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<UserProjectModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectsController_GetByIdAsync_ReturnsNotFound()
        {
            //arrange
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + id);

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserProjectsController_GetById_ReturnsPositionModel()
        {
            //arrange
            var expected = PositionModels.First();

            //act
            var httpResponse = await _client.GetAsync(RequestUri + $"positions/{1}");

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<PositionModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserProjectsController_GetByIdAsync_ReturnsNotFoundStatus()
        {
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + $"positions/{id}");

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserProjectsController_Add_AddsUserProjectToDb()
        {
            //arrange
            var userProject = new UserProjectModel
            {
                Id = 6,
                PositionId=1,
                PositionName="Name1",
                TaskId=4,
                TaskName="Name4",
                UserId=2,
                UserName="Username2",
                UserEmail = "email2@gmail.com"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(userProject), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri+ "project", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.UserProjects.Should().HaveCount(6);
            }

        }

        [Test]
        public async Task UserProjectsController_Add_AddsUserProjectsToDb()
        {
            //arrange
            var userProject = new List<UserProjectModel> 
            {
                new UserProjectModel
                {
                    Id = 6,
                    PositionId = 1,
                    PositionName = "Name1",
                    TaskId = 4,
                    TaskName = "Name4",
                    UserId = 2,
                    UserName = "Username2",
                    UserEmail="email2@gmail.com"
                },
                new UserProjectModel 
                {
                    Id = 7,
                    PositionId = 2,
                    PositionName = "Name2",
                    TaskId = 4,
                    TaskName = "Name4",
                    UserId = 2,
                    UserName = "Username2",
                    UserEmail="email2@gmail.com"
                }
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(userProject), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.UserProjects.Should().HaveCount(7);
            }

        }

        [Test]
        public async Task UserProjectsController_Add_AddsPositionToDb()
        {
            //arrange
            var position = new PositionModel 
            { 
                Id = 4, 
                Name = "Name4", 
                Description = "Description4" 
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "positions", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Positions.Should().HaveCount(4);
            }
        }

        [Test]
        public async Task UserProjectsController_Add_ThrowsExceptionIfPositionIsInvalid()
        {
            //arrange
            var position = new PositionModel
            {
                Id = 4,
                Name = "",
                Description = "Description4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "positions", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UserProjectsController_Update_UpdatesUserProjectInDb()
        {
            //arrange
            var project = new UserProjectModel
            {
                Id = 1,
                PositionId = 1,
                PositionName = "Name1",
                TaskId = 4,
                TaskName = "Name4",
                UserId = 2,
                UserName = "Username2",
                UserEmail = "email2@gmail.com"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri+ "project", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                if (context != null)
                {
                    context.UserProjects.Should().HaveCount(5);

                    var DbModel = await context.UserProjects.FindAsync(1);
                    DbModel.Should().NotBeNull().And.BeEquivalentTo(project, options =>
                        options.Excluding(x => x.Id).ExcludingMissingMembers());
                }
            }
        }

        [Test]
        public async Task UserProjectsController_Update_UpdatesPositionInDb()
        {
            //arrange
            var position = new PositionModel
            {
                Id = 1,
                Name = "Name4",
                Description = "Description4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "positions", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                if (context != null)
                {
                    context.Positions.Should().HaveCount(3);

                    var DbModel = await context.Positions.FindAsync(1);
                    DbModel.Should().NotBeNull().And.BeEquivalentTo(position, options =>
                        options.Excluding(x => x.Id).ExcludingMissingMembers());
                }
            }
        }

        [Test]
        public async Task UserProjectsController_Update_ThrowsExceptionIfPositionIsInvalid()
        {
            //arrange
            var position = new PositionModel
            {
                Id = 1,
                Name = "",
                Description = "Description4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(position), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "positions", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UserProjectsController_Delete_DeletesUserProjectFromDb()
        {
            var httpResponse = await _client.DeleteAsync(RequestUri +"?ids=1&ids=2");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.UserProjects.Should().HaveCount(3);
            }
        }

        [Test]
        public async Task UserProjectsController_Delete_DeletesPositionFromDb()
        {
            // arrange
            var id = 1;

            // act
            var httpResponse = await _client.DeleteAsync(RequestUri + $"positions/{id}");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Positions.Should().HaveCount(2);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

        private readonly UserProjectModel UserProjectModel = 
            new UserProjectModel
            {
                Id = 1,
                PositionId = 1,
                TaskId = 1,
                UserId = 1
            };

        private readonly IEnumerable<UserProjectModel> UserProjectModels =
            new List<UserProjectModel>
            {
                new UserProjectModel
                {
                    Id = 1,
                    PositionId = 1,
                    PositionName = "Name1",
                    TaskId = 1,
                    TaskName="Name1",
                    UserId=1,
                    UserName="Username1",
                    UserEmail="email1@gmail.com"
                },
                new UserProjectModel
                {
                    Id = 2,
                    PositionId = 2,
                    PositionName = "Name2",
                    TaskId = 2,
                    TaskName="Name2",
                    UserId=1,
                    UserName="Username1",
                    UserEmail="email1@gmail.com"
                },
                new UserProjectModel
                {
                    Id = 3,
                    PositionId = 3,
                    PositionName = "Name3",
                    TaskId = 3,
                    TaskName="Name3",
                    UserId=1,
                    UserName="Username1",
                    UserEmail="email1@gmail.com"
                },
                new UserProjectModel
                {
                    Id = 4,
                    PositionId = 2,
                    PositionName = "Name2",
                    TaskId = 4,
                    TaskName="Name4",
                    UserId=1,
                    UserName="Username1",
                    UserEmail="email1@gmail.com"
                },
                new UserProjectModel
                {
                    Id = 5,
                    PositionId = 1,
                    PositionName = "Name1",
                    TaskId = 3,
                    TaskName="Name3",
                    UserId=1,
                    UserName="Username1",
                    UserEmail="email1@gmail.com"
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
