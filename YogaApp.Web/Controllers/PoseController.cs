using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Web.Extensions.Pose;
using YogaApp.Web.Models;

namespace YogaApp.Web.Controllers;

public class PoseController : Controller
{
    private readonly ILogger<PoseController> _logger;
    private readonly IApplicationServices _services;
    public PoseController(ILogger<PoseController> logger, IApplicationServices services)
    {
        _logger = logger;
        _services = services;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //get list of GetAllPosesResponse DTOs
        var posesDto = await _services.GetAllPosesAsync();
        
        //use extension method to map poses to view model by difficulty
        var posesView = posesDto.ToViewAllModel();
        
        return View(posesView);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        //get GetPoseByIdResponse DTO
        var poseDto = await _services.GetPoseByIdAsync(id);
        // map onto View Model using extension
        var poseView = poseDto.ToViewDetailModel();
        
        return View(poseView);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> UpdatePose(int id)
    {
        var poseDto = await _services.UpdatePoseAsync(id);
        //map to View Model using extension
        var pose = poseDto.ToViewUpdateModel();
        
        return View(pose);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePose(int id, UpdatePoseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await RepopulateFormOptions(model);
            return View(model);
        }
        
        
        
        
        return View("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateNewPose()
    {
        
        var pose = new CreatePoseViewModel();
        await PopulateDropdownsAsync(pose);
        
        return View(pose);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPose(CreatePoseViewModel pose)
    {
        
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Creating new pose failed. Model invalid");
            await PopulateDropdownsAsync(pose);
            return View("CreateNewPoseGet", pose);
        }

        var request = new CreatePoseRequestDto
        {
            PoseName = pose.PoseName,
            SanskritName = pose.SanskritName,
            TranslationOfName = pose.TranslationOfName,
            PoseDescription = pose.PoseDescription,
            PoseBenefits = pose.PoseBenefits,
            DifficultyId = pose.DifficultyId,
            UrlSvg = pose.UrlSvg,
            ThumbnailUrlSvg = pose.UrlSvgAlt,
            CategoryIds = pose.CategoryIds
        };

        try
        {
            var poseId = await _services.CreatePoseInDbAsync(request);
            return RedirectToAction("Detail", new { id = poseId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            ModelState.AddModelError("", ex.Message);
            await PopulateDropdownsAsync(pose);
            return View("CreateNewPoseGet", pose);
        }
        
    }
    
    //helper method to rebuild dropdown/checkbox options in failed Creates
    private async Task PopulateDropdownsAsync(CreatePoseViewModel pose)
    {
        // load difficulties and categories
        var difficulties = await _services.GetAllDifficultiesAsync();
        pose.DifficultyOptions = difficulties.Select(d => new SelectListItem
        {
            Value = d.DifficultyId.ToString(),
            Text = d.DifficultyLevel
        }).ToList();

        var categories = await _services.GetAllCategoriesAsync();
        pose.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName
        }).ToList();
    }
    
    // Helper method to rebuild dropdown/checkbox options in failed Updates
    private async Task RepopulateFormOptions(UpdatePoseViewModel viewModel)
    {
        var difficulties = await _services.GetAllDifficultiesAsync();
        viewModel.DifficultyOptions = difficulties.Select(d => new SelectListItem
        {
            Value = d.DifficultyId.ToString(),
            Text = d.DifficultyLevel,
            Selected = d.DifficultyId == viewModel.DifficultyId // Preserve user's selection
        }).ToList();

        var categories = await _services.GetAllCategoriesAsync();
        viewModel.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName,
            Selected = viewModel.CategoryIds?.Contains(c.CategoryId) == true
        }).ToList();
    }

}