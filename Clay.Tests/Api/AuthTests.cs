using Clay.Api.Controllers;
using Clay.Api.Requests;
using Clay.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Clay.Tests.Api
{
    [TestClass]
    public class AuthTests
    {
        readonly private Mock<IAuthenticationService> _authenticationServiceMock;
        readonly private AuthController _authController;

        public AuthTests()
        {
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _authController = new AuthController(_authenticationServiceMock.Object);
        }

        [TestMethod]
        public async Task TestUserLoginInvalidUsername()
        {
            // Arrange
            var invalidUsernameRequest = new LoginRequest { username = "invalidUsername", password = "validPassword" };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync((string)null);

            // Act
            var result = await _authController.login(invalidUsernameRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public async Task TestUserLoginInvalidPassword()
        {
            // Arrange
            var invalidPasswordRequest = new LoginRequest { username = "validUsername", password = "invalidPassword" };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync((string)null);

            // Act
            var result = await _authController.login(invalidPasswordRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public async Task TestUserLogin()
        {
            var token = "MockToken";
            // Arrange
            var validRequest = new LoginRequest {username = "validUsername", password = "validPassword" };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(token);

            // Act
            var result = await _authController.login(validRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task TestUserLoginMissingUsername()
        {
            // Arrange
            var missingUsernameRequest = new LoginRequest { password = "validPassword" };

            // Act
            var result = await _authController.login(missingUsernameRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public async Task TestUserLoginMissingPassword()
        {
            // Arrange
            var missingPasswordRequest = new LoginRequest { username = "validUsername" };

            // Act
            var result = await _authController.login(missingPasswordRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        }
    }
}
