using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.Services;
using YogaApp.Web.Models;

namespace YogaApp.Web.Controllers;

public class PoseController : Controller
{
    private readonly ILogger<PoseController> _logger;

    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IDifficultyRepository _difficultyRepo;
    private readonly CreatePoseUseCase _createPoseUseCase;

    public PoseController(ILogger<PoseController> logger, IPoseRepository poseRepo, ICategoryRepository categoryRepo,
        IDifficultyRepository difficultyRepo, CreatePoseUseCase createPoseUseCase)
    {
        _logger = logger;
        _poseRepo = poseRepo;
        _categoryRepo = categoryRepo;
        _difficultyRepo = difficultyRepo;
        _createPoseUseCase = createPoseUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var poses = await _poseRepo.GetAllPosesAsync();
        //map DTO to AllPosesViewModel
        //return View(model)
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateNewPoseGet()
    {
        var categories = await _categoryRepo.GetAllCategoriesAsync();
        var difficulties = await _difficultyRepo.GetAllDifficultiesAsync();
        var pose = new CreatePoseViewModel();
        await PopulateDropdownsAsync(pose);
        
        return View(pose);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewPosePost(CreatePoseViewModel pose)
    {
        if (!ModelState.IsValid)
        {
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
            var poseId = await _createPoseUseCase.ExecuteCreatePoseAsync(request);
            return RedirectToAction("Index");//@TODO change this to a view of the table
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            await PopulateDropdownsAsync(pose);
            return View("CreateNewPoseGet", pose);
        }
        
    }
    //helper method
    private async Task PopulateDropdownsAsync(CreatePoseViewModel pose)
    {
        // load difficulties and categories
        var difficulties = await _difficultyRepo.GetAllDifficultiesAsync();
        pose.DifficultyOptions = difficulties.Select(d => new SelectListItem
        {
            Value = d.Difficulty_Id.ToString(),
            Text = d.Difficulty_Level
        }).ToList();

        var categories = await _categoryRepo.GetAllCategoriesAsync();
        pose.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.Category_Id.ToString(),
            Text = c.Category_Name
        }).ToList();
    }
}