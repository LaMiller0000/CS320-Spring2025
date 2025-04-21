using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCookBookApp.Controllers
{
    [Route("YouTube/Player")]

    public class YouTubeController : Controller
    {
        public IActionResult Player()
        {
            var video = new YouTubeVideoModel
            {
                VideoId = "playlist?list=PLopY4n17t8RD-xx0UdVqemiSa0sRfyX19&si=oYyrtGo-q9vE1z90",
                Title = "Basic Cooking Playlist",
                Description = "A selected playlist to help you learn to cook, and to help your own kitchen."
            };

            return View(video);
        }
    }
}