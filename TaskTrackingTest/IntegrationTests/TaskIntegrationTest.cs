using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.IntegrationTests
{
    public class TaskIntegrationTest
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/tasks/";

        [SetUp]
        public void Init()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task TasksContoller_GetAll_ReturnsAllTaskModels()
        {
            // Arrange
            var expected = TaskModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<TaskModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task TasksContoller_GetAll_ReturnsAllStatusModels()
        {
            // Arrange
            var expected = StatusModels;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "statuses");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<StatusModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task TasksController_GetById_ReturnsTaskModel()
        {
            //arrange
            var expected = TaskModels.First();

            //act
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<TaskModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task TasksController_GetByIdAsync_ReturnsNotFound()
        {
            //arrange
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + id);

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task TasksController_GetById_ReturnsStatusModel()
        {
            //arrange
            var expected = StatusModels.First();

            //act
            var httpResponse = await _client.GetAsync(RequestUri + $"statuses/{1}");

            //assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<StatusModel>(stringResponse);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task TasksController_GetByIdAsync_ReturnsNotFoundStatus()
        {
            var id = 10099;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + $"statuses/{id}");

            // assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task TasksController_Add_AddsTaskToDb()
        {
            //arrange
            var task = new TaskModel
            {
                Id = 6,
                Name = "Name4",
                Description = "Description4",
                StartDate = new DateTime(2022, 1, 1),
                ExpiryDate = new DateTime(2022, 1, 7),
                StatusId = 1,
                StatusName = "Status1",
                ManagerId = 2,
                ManagerUserName="Username2",
                ProjectId=1,
                ProjectName="Name1",
                UserProjectIds=new List<int>()
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.Assignments.Should().HaveCount(5);
            }

        }

        [Test]
        public async Task TasksController_Add_ThrowsExceptionIfModelIsInvalid()
        {
            //arrange
            var task = new TaskModel
            {
                Id = 5,
                Name = "",
                Description = "Description4",
                StartDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                StatusId = 1,
                StatusName = "Status1",
                ManagerId = 2,
                ManagerUserName = "Username2",
                ProjectId = 1,
                ProjectName = "Name1",
                UserProjectIds = new List<int>()
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TasksController_Add_AddsStatusToDb()
        {
            //arrange
            var status = new StatusModel
            {
                Id = 4,
                Name = "Name4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "statuses", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.AssignmentStatuses.Should().HaveCount(4);
            }
        }

        [Test]
        public async Task TasksController_Add_ThrowsExceptionIfStatusIsInvalid()
        {
            //arrange
            var status = new StatusModel
            {
                Id = 4,
                Name = ""
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "statuses", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TasksController_Update_UpdatesTaskInDb()
        {
            //arrange
            var task = new TaskModel
            {
                Id = 1,
                Name = "Name4",
                Description = "Description4",
                StartDate = new DateTime(2022, 1, 2),
                ExpiryDate = new DateTime(2022, 1, 5),
                StatusId = 1,
                StatusName = "Status1",
                ManagerId = 2,
                ManagerUserName = "Username2",
                ProjectId = 1,
                ProjectName = "Name1",
                UserProjectIds = new List<int>()
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                if (context != null)
                {
                    context.Assignments.Should().HaveCount(4);

                    var DbModel = await context.Assignments.FindAsync(1);
                    DbModel.Should().NotBeNull().And.BeEquivalentTo(task, options =>
                        options.Excluding(x => x.Id).ExcludingMissingMembers());
                }
            }
        }

        [Test]
        public async Task TasksController_Update_ThrowsExceptionIfModelIsInvalid()
        {
            //arrange
            //empty name
            var task = new TaskModel
            {
                Id = 1,
                Name = "",
                Description = "Description4",
                StartDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                StatusId = 1,
                StatusName = "Status1",
                ManagerId = 2,
                ManagerUserName = "Username2",
                ProjectId = 1,
                ProjectName = "Name1",
                UserProjectIds = new List<int>()
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TasksController_Update_UpdatesStatusInDb()
        {
            //arrange
            var status = new StatusModel
            {
                Id = 1,
                Name = "Status4"
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "statuses", content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                if (context != null)
                {
                    context.AssignmentStatuses.Should().HaveCount(3);

                    var DbModel = await context.AssignmentStatuses.FindAsync(1);
                    DbModel.Should().NotBeNull().And.BeEquivalentTo(status, options =>
                        options.Excluding(x => x.Id).ExcludingMissingMembers());
                }
            }
        }

        [Test]
        public async Task TasksController_Update_ThrowsExceptionIfStatusIsInvalid()
        {
            //arrange
            var status = new StatusModel
            {
                Id = 1,
                Name = ""
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + "statuses", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TasksController_Delete_DeletesTaskFromDb()
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
                context.Assignments.Should().HaveCount(3);
            }
        }

        [Test]
        public async Task TasksController_Delete_DeletesStatusFromDb()
        {
            // arrange
            var id = 1;

            // act
            var httpResponse = await _client.DeleteAsync(RequestUri + $"statuses/{id}");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<TaskDbContext>();
                context.AssignmentStatuses.Should().HaveCount(2);
            }
        }

        [Test]
        public async Task ProjectsContoller_GetAll_ReturnsAllTUsersByTaskIdModels()
        {
            // Arrange
            var expected = UserModels;

            var id = 1;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{id}/users");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<UserModel>>(stringResponse);
            actual.Should().BeEquivalentTo(expected, t=>
                t.Excluding(t=>t.UserProjectIds).Excluding(t=>t.TaskIds));
        }

        //[Test]
        //public async Task TasksController_Update_UpdatesTaskStatusInDb()
        //{
        //    //arrange
        //    var status = new StatusModel { Id = 2, Name = "Status2" };

        //    var expected = new TaskModel
        //    {
        //        Id = 1,
        //        Name = "Name1",
        //        Description = "Description1",
        //        StartDate = new DateTime(2022, 1, 1),
        //        ExpiryDate = new DateTime(2022, 1, 7),
        //        StatusId = 2,
        //        StatusName = "Status2",
        //        ManagerId = 2,
        //        ManagerUserName = "Username2",
        //        ProjectId = 1,
        //        ProjectName = "Name1",
        //        UserProjectIds = new List<int>() { 1 }
        //    };

        //    //act
        //    var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
        //    var httpResponse = await _client.PutAsync(RequestUri+$"{1}/status", content);

        //    //assert
        //    httpResponse.EnsureSuccessStatusCode();
        //    using (var test = _factory.Services.CreateScope())
        //    {
        //        var context = test.ServiceProvider.GetService<TaskDbContext>();
        //        if (context != null)
        //        {
        //            context.Assignments.Should().HaveCount(4);

        //            var DbModel = await context.Assignments.FindAsync(1);
        //            DbModel.Should().NotBeNull().And.BeEquivalentTo(expected, options =>
        //                options.Excluding(x => x.Id).ExcludingMissingMembers());
        //        }
        //    }
        //}

        [Test]
        public async Task TasksController_Update_ThrowsExceptionIfTaskStatusIsInvalid()
        {
            //arrange
            var status = new StatusModel
            {
                Id = 1,
                Name = ""
            };

            //act
            var content = new StringContent(JsonConvert.SerializeObject(status), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + $"{1}/status", content);

            //assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

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

        private readonly IEnumerable<UserModel> UserModels =
            new List<UserModel>
            {
                new UserModel {
                    Id = 1,
                    FirstName = "Firstname1",
                    LastName = "Lastname1",
                    UserName = "Username1",
                    Email = "email1@gmail.com",
                    UserProjectIds=new List<int>(){1}
                }
            };

        private readonly IEnumerable<StatusModel> StatusModels =
            new List<StatusModel>
            {
                new StatusModel { Id = 1, Name = "Status1" },
                new StatusModel { Id = 2, Name = "Status2" },
                new StatusModel { Id = 3, Name = "Status3" }
            };
    }
}
