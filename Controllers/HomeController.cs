using Cloud1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cloud1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
     private readonly IHttpContextAccessor _httpContextAccessor;
    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Index()
    {
        int? userId = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");
        ViewData["UserID"] = userId;
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult MyWork()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            }
        );
    }
}

