using Microsoft.AspNetCore.Mvc;
using Xunit;
using MyCookBookApp.Controllers;
using MyCookBookApp.Models;

namespace MyCookBookApp.Tests
{
    public class YouTubeControllerTests
    {
        [Fact]
        public void Player_ReturnsViewResult_WithCorrectModel()
        {
            var controller = new YouTubeController();

            var result = controller.Player() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            
            var model = result.Model as YouTubeVideoModel;
            Assert.NotNull(model);
            Assert.Equal("playlist?list=PLopY4n17t8RD-xx0UdVqemiSa0sRfyX19&si=eLDjY2mtsyfRcpri", model.VideoId);
            Assert.Equal("Basic Cooking Videos", model.Title);
            Assert.Equal("A helpful playlist for cooking basics.", model.Description);
        }
    }
}