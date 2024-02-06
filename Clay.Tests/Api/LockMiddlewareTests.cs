using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Clay.Api.Controllers;
using Clay.Api.Middleware;
using Clay.Domain.Services;

namespace Clay.Tests.Api
{
    [TestClass]
    public class LockMiddlewareTests
    {
        [TestMethod]
        public async Task TestUnlockDoorUnauthorizedAccess()
        {
            // Arrange
            var lockServiceMock = new Mock<ILockService>();
            var authServiceMock = new Mock<IAuthenticationService>();

            var middleware = new LockAuthorizationMiddleware(innerHttpContext =>
            {
                // Simulate the invocation of the next middleware in the pipeline
                return Task.CompletedTask;
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Items["UserId"] = null;
            httpContext.Request.RouteValues["lockId"] = "1";

            // Act
            await middleware.Invoke(httpContext, lockServiceMock.Object, authServiceMock.Object);

            // Assert
            Assert.AreEqual(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
        }

        [TestMethod]
        public async Task TestUnlockDoorMissingBearerToken()
        {
            // Arrange
            var lockServiceMock = new Mock<ILockService>();
            var authServiceMock = new Mock<IAuthenticationService>();

            var middleware = new LockAuthorizationMiddleware(innerHttpContext =>
            {
                // Simulate the invocation of the next middleware in the pipeline
                return Task.CompletedTask;
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.RouteValues["lockId"] = "1";

            // Act
            await middleware.Invoke(httpContext, lockServiceMock.Object, authServiceMock.Object);

            // Assert
            Assert.AreEqual(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
        }

        [TestMethod]
        public async Task TestUnlockDoorUserRoleDoesNotHaveAccess()
        {
            // Arrange
            var lockServiceMock = new Mock<ILockService>();
            var authServiceMock = new Mock<IAuthenticationService>();

            lockServiceMock.Setup(service => service.CanUserAccessLock(123, 1)).Returns(false);

            var middleware = new LockAuthorizationMiddleware(innerHttpContext =>
            {
                // Simulate the invocation of the next middleware in the pipeline
                return Task.CompletedTask;
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Items["UserId"] = 123;
            httpContext.Request.RouteValues["lockId"] = "1";

            // Act
            await middleware.Invoke(httpContext, lockServiceMock.Object, authServiceMock.Object);

            // Assert
            Assert.AreEqual(StatusCodes.Status401Unauthorized, httpContext.Response.StatusCode);
        }
    }
}
