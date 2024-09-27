using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class UrlController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UrlService _urlService;

    public UrlController(ILogger<HomeController> logger, UrlService urlService)
    {
        _logger = logger;
        _urlService = urlService;
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    public IActionResult Edit()
    {
        return View();
    }
    
    public IActionResult Delete(int id)
    {
        var model = _urlService.GetAsync(id).Result;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Url url)
    {
        await _urlService.CreateAsync(url.OriginalUrl);
        return RedirectToAction("Index","Home");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var url = await _urlService.GetAsync(id);
        return View(url);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Url url)
    {
        await _urlService.UpdateAsync(url);
        return RedirectToAction("Index","Home");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        await _urlService.DeleteAsync(id);
        return RedirectToAction("Index","Home");
    }
}