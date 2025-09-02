using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCaseInterfaces;

namespace YogaApp.Web.Controllers;

public class SearchController : Controller
{
    
    private readonly ILogger<PoseController> _logger;
    private readonly IApplicationServices _services;
    public SearchController(ILogger<PoseController> logger, IApplicationServices services)
    {
        _logger = logger;
        _services = services;
    }
    // GET
    public async Task<IActionResult> Index(string searchString)
    {
        //deals with empty search
        if (string.IsNullOrEmpty(searchString))
        {
            return RedirectToAction("Index", "Home");
        }

        //var result = await _services.Search(searchString);
        //var poseView = result.ToSearchViewModel;
        //return View(poseView);
        return View();
    }
}