using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingTest.BusinessTests
{
    public class AuthServiceTests
    {
        [Test]
        public async Task UserService_GetToken_ReturnsToken()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(t=>t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t=>t.Email== "email1@gmail.com"));

            fakeUserManager.Setup(t => t.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            fakeUserManager.Setup(t => t.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RoleEntities.Where(t => t.Name == "Name1").Select(t => t.Name).ToList());

            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var mockGenerateTokenService = new Mock<IGenerateTokenService>();

            mockGenerateTokenService.Setup(t => t.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
                .Returns("1q2w.3e4r5t6y7u.8i9o");

            mockGenerateTokenService.Setup(t => t.GenerateRefreshToken())
                .Returns("1q2w3e4r5t6y7u8i9o");

            var expected = TokenModel;

            var authService = new AuthService(fakeUserManager.Object, mockGenerateTokenService.Object, null);

            //Act
            var actual = await authService.Login(loginModel);

            //Assert
            fakeUserManager.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UserService_GetToken_ThrowsTaskTrackingExceptionWithEmptyLogin()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            fakeUserManager.Setup(t => t.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            fakeUserManager.Setup(t => t.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RoleEntities.Where(t => t.Name == "Name1").Select(t => t.Name).ToList());

            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var mockGenerateTokenService = new Mock<IGenerateTokenService>();

            mockGenerateTokenService.Setup(t => t.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
                .Returns("1q2w.3e4r5t6y7u.8i9o");

            mockGenerateTokenService.Setup(t => t.GenerateRefreshToken())
                .Returns("1q2w3e4r5t6y7u8i9o");

            var expected = TokenModel;

            var authService = new AuthService(fakeUserManager.Object, mockGenerateTokenService.Object, null);

            var loginModel = new LoginModel
            {
                Login=string.Empty,
                Password= "*1aApassword"
            };

            //Act
            Func<Task> act = async () => await authService.Login(loginModel);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        [Test]
        public async Task UserService_GetToken_ThrowsTaskTrackingExceptionWithEmptyPassword()
        {
            //Arrange
            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(UserEntities.FirstOrDefault(t => t.Email == "email1@gmail.com"));

            fakeUserManager.Setup(t => t.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            fakeUserManager.Setup(t => t.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RoleEntities.Where(t => t.Name == "Name1").Select(t => t.Name).ToList());

            fakeUserManager.Setup(m => m.UpdateAsync(It.IsAny<User>()));

            var mockGenerateTokenService = new Mock<IGenerateTokenService>();

            mockGenerateTokenService.Setup(t => t.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
                .Returns("1q2w.3e4r5t6y7u.8i9o");

            mockGenerateTokenService.Setup(t => t.GenerateRefreshToken())
                .Returns("1q2w3e4r5t6y7u8i9o");

            var expected = TokenModel;

            var authService = new AuthService(fakeUserManager.Object, mockGenerateTokenService.Object, null);

            var loginModel = new LoginModel
            {
                Login = "email1@gmail.com",
                Password = string.Empty
            };

            //Act
            Func<Task> act = async () => await authService.Login(loginModel);

            //Assert
            await act.Should().ThrowAsync<TaskTrackingException>();
        }

        private static TokenModel TokenModel =>
            new TokenModel { AccessToken = "1q2w.3e4r5t6y7u.8i9o", RefreshToken = "1q2w3e4r5t6y7u8i9o" };

        private static LoginModel loginModel =>
            new LoginModel { Login = "email1@gmail.com", Password = "*1aApassword" };

        private static IEnumerable<User> UserEntities =>
            new List<User>
            {
                new User { Id = 1, FirstName = "Firstname1", LastName = "Lastname1", UserName = "Username1", Email = "email1@gmail.com" },
                new User { Id = 2, FirstName = "Firstname2", LastName = "Lastname2", UserName = "Username2", Email = "email2@gmail.com" },
                new User { Id = 3, FirstName = "Firstname3", LastName = "Lastname3", UserName = "Username3", Email = "email3@gmail.com" }
            };

        private static IEnumerable<IdentityRole<int>> RoleEntities =>
            new List<IdentityRole<int>>
            {
                new IdentityRole<int>{Id=1,Name="Name1",NormalizedName="NAME1"},
                new IdentityRole<int>{Id=2,Name="Name2",NormalizedName="NAME2"},
                new IdentityRole<int>{Id=3,Name="Name3",NormalizedName="NAME3"}
            };
    }
}
