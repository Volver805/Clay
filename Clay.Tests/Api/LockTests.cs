using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Clay.Domain.Services;
using Clay.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Clay.Tests.Api
{
    [TestClass]
    public class LockTests
    {
        [TestMethod]
        public async Task TestLockDoor()
        {
            var lockServiceMock = new Mock<ILockService>();
            lockServiceMock.Setup(service => service.UpdateLockStatus(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()));

            var controller = new LockController(lockServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.HttpContext.Items["UserId"] = 123;

            // Act
            var result = await controller.Lock(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            lockServiceMock.Verify(service => service.UpdateLockStatus(1, true, 123), Times.Once);
        }

        [TestMethod]
        public async Task TestUnlockDoor()
        {
            // Arrange
            var lockServiceMock = new Mock<ILockService>();
            lockServiceMock.Setup(service => service.UpdateLockStatus(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<int>()));

            var controller = new LockController(lockServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controller.HttpContext.Items["UserId"] = 123;

            // Act
            var result = await controller.Unlock(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            lockServiceMock.Verify(service => service.UpdateLockStatus(1, false, 123), Times.Once);
        }
    }
}
