using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using MyCookBookApp.Models;

namespace MyCookBookApp.Controllers;

public class Home : Controller
{
    private readonly ILogger<Home> _logger;

    public Home(ILogger<Home> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(_logger);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Recipe()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
