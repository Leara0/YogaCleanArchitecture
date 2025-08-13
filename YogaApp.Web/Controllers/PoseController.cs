using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
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
        //filter by difficulty level and map to View Model
        var easy = posesDto
            .Where(p => p.Difficulty_Id == 1)
            .Select(p => new AllPosesViewModel(p)).ToList();
        var medium = posesDto
            .Where(p => p.Difficulty_Id == 2)
            .Select(p => new AllPosesViewModel(p)).ToList();
        var hard = posesDto
            .Where(p => p.Difficulty_Id == 3)
            .Select(p => new AllPosesViewModel(p)).ToList();
        
        //map it to one model (PosesByDifficultyViewModel
        var posesView = new PosesByDifficultyViewModel()
        {
            EasyPoses = easy,
            MediumPoses = medium,
            HardPoses = hard
        };
        return View(posesView);
    }

    [HttpGet]
    public async Task<IActionResult> Detail(int id)
    {
        //get GetPoseByIdResponse DTO
        var poseDto = await _services.GetPoseByIdAsync(id);
        // map onto View Model
        var poseView = new PoseDetailsViewModel(poseDto);
        
        return View(poseView);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> UpdatePoseGet(int id)
    {
        _logger.LogInformation($"Getting pose {id}");
        var poseDto = await _services.UpdatePoseAsync(id);
        _logger.LogInformation($"Pose name is {poseDto.PoseName}");
        var pose = new CreatePoseViewModel(poseDto);
        
        await PopulateDropdownsAsync(pose);
        
        return View(pose);
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateNewPoseGet()
    {
        
        var pose = new CreatePoseViewModel();
        await PopulateDropdownsAsync(pose);
        
        return View(pose);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPosePost(CreatePoseViewModel pose)
    {
        
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Creating new pose failed. Model invalid");
            await PopulateDropdownsAsync(pose);
            return View("CreateNewPoseGet", pose);
        }

        var request = new CreatePoseRequest
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
    
    //helper method
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

    private async Task PopulatePreselectedDropdownsAsync(CreatePoseViewModel pose)
    {
        // load difficulties and categories
        var difficulties = await _services.GetAllDifficultiesAsync();
        pose.DifficultyOptions = difficulties.Select(d => new SelectListItem
        {
            Value = d.DifficultyId.ToString(),
            Text = d.DifficultyLevel,
            Selected = d.DifficultyId == pose.DifficultyId
        }).ToList();

        var categories = await _services.GetAllCategoriesAsync();
        pose.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName,
            Selected = pose.CategoryIds.Contains(c.CategoryId)
        }).ToList();
    }
}