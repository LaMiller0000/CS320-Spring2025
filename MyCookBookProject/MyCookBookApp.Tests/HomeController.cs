using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using MyCookBookApp.Controllers;
using MyCookBookApp.Models;

namespace MyCookBookApp.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<Home>> _loggerMock;
        private readonly Home _controller;

        public HomeControllerTests()
        {
            _loggerMock = new Mock<ILogger<Home>>();
            _controller = new Home(_loggerMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            var result = _controller.Privacy();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Recipe_ReturnsViewResult()
        {
            var result = _controller.Recipe();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsViewResultWithErrorViewModel()
        {
            var result = _controller.Error() as ViewResult;
            var model = result.Model as ErrorViewModel;

            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.NotNull(model.RequestId);
        }
    }
}