using Microsoft.AspNetCore.Mvc;
using System;
using WebApiForClients.Controllers;
using Xunit;

namespace UnitTest.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexResultDataMessage()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello!", result?.ViewData["Message"]);
        }
    }
}
