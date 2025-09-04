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

    //GET
    public async Task<IActionResult> AiSequenceResults(string searchTerm)
    {
        var viewModel = new AiSequenceViewModel { SearchTerm = searchTerm };

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return RedirectToAction("Index", "Home");
        }

        try
        {
            //get the results
            var posesDto = await _services.GetAiSuggestionsAsync(searchTerm);
            //map using the extension
            viewModel = posesDto.ToAiSequenceViewModel();
            //check if there are any results
            viewModel.HasResults = viewModel.SuggestedPoses.Any();

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"AI sequence generation failed for goal: {searchTerm}");
            viewModel.ErrorMessage = "Unable to generate AI suggestions right now. Please try again later.";
            return View(viewModel);
        }

        _logger.LogInformation($"Searching for {searchTerm}");
    }
}