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
                VideoId = "https://youtu.be/1On-ZHfTWOo?si=45Z_8LYxsIMKtqKv", // Replace with an actual YouTube Video ID
                Title = "Sample YouTube Video",
                Description = "This is a sample description for the YouTube video."
            };

            return View(video); // Pass the model to the view
        }
    }
}