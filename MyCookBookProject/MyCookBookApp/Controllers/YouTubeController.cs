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
            // Create a sample video model
            var video = new YouTubeVideoModel
            {
                VideoId = "playlist?list=PLopY4n17t8RD-xx0UdVqemiSa0sRfyX19&si=oYyrtGo-q9vE1z90", // Replace with an actual YouTube Video ID
                Title = "Basic Cooking Playlist",
                Description = "A selected playlist to help you learn to cook, and to help your own kitchen."
            };

            return View(video); // Pass the model to the view
        }
    }
}