using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Extensions;

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
    public async Task<IActionResult> Index(string searchTerm)
    {
        //deals with empty search
        _logger.LogInformation($"failed to capture search string: {searchTerm}");
        if (string.IsNullOrEmpty(searchTerm))
        {
            return RedirectToAction("Index", "Home");
        }

        _logger.LogInformation($"Searching for {searchTerm}");
        
        //call use case to search repos for search string
        var resultDto = await _services.SearchAsync(searchTerm);
        //use mapping extension to map DTO to view model
        var viewModel = resultDto.ToSearchViewModel();
        viewModel.SearchTerm = searchTerm;
       
        return View(viewModel);
    }
}