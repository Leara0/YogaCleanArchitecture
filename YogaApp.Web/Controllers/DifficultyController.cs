using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Extensions;
using YogaApp.Web.Models;

namespace YogaApp.Web.Controllers;

public class DifficultyController : Controller
{
    private readonly ILogger<DifficultyController> _logger;
    private readonly IApplicationServices _appServices;

    public DifficultyController(ILogger<DifficultyController> logger, IApplicationServices appServices)
    {
        _logger = logger;
        _appServices = appServices;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> Details(int diffId)
    {
        var difficulty = await _appServices.GetDifficultyByIdAsync(diffId);
        var diffModel = difficulty.ToDifficultyViewModel();
        return View(diffModel);
    }
}