using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UrlService _urlService;

    public HomeController(ILogger<HomeController> logger, UrlService urlService)
    {
        _logger = logger;
        _urlService = urlService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _urlService.GetAllAsync();
        return View(model.ToList());
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}