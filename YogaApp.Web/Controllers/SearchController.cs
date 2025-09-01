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
    public IActionResult Index()
    {
        return View();
    }
}