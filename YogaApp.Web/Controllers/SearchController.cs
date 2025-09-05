using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Extensions;
using YogaApp.Web.Models;

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

        if (string.IsNullOrEmpty(searchTerm))
        {
            _logger.LogInformation($"failed to capture search string: {searchTerm}");
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

    [HttpGet]
    public async Task<IActionResult> AiSequence()
    {
        return View(new AiSequenceViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> AiSequence(AiSequenceViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.SearchTerm))
        {
            ModelState.AddModelError("SearchTerm", "Please enter a goal");
            return View(model);
        }

        try
        {
            //get the results
            var posesDto = await _services.GetAiSuggestionsAsync(model.SearchTerm);
            //map the suggested poses from dto to view model using extension
            model.SuggestedPoses = posesDto.ToAiSequenceViewModel();
            //check if there are any results
            model.HasResults = true;

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"AI sequence generation failed for goal: {model.SearchTerm}");
            model.ErrorMessage = "Unable to generate AI suggestions right now. Please try again later.";
            return View(model);
        }
    }
}