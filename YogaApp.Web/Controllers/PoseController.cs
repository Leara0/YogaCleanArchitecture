using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.UseCases;
using YogaApp.Web.Models;

namespace YogaApp.Web.Controllers;

public class PoseController : Controller
{
    private readonly ILogger<PoseController> _logger;

    
    private readonly GetAllCategoriesUseCase _getAllCategories;
    private readonly GetAllDifficultiesUseCase _getAllDifficulties;
    private readonly CreatePoseUseCase _createPoseUseCase;
    private readonly  GetAllPosesUseCase _getAllPosesUseCase;

    public PoseController(ILogger<PoseController> logger, GetAllCategoriesUseCase getAllCategories,
        GetAllDifficultiesUseCase getAllDifficulties, CreatePoseUseCase createPoseUseCase, GetAllPosesUseCase getAllPosesUseCase)
    {
        _logger = logger;
        
        _getAllCategories = getAllCategories;
        _getAllDifficulties = getAllDifficulties;
        _createPoseUseCase = createPoseUseCase;
        _getAllPosesUseCase = getAllPosesUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var posesDto = await _getAllPosesUseCase.ExecuteGetAllPosesAsync();
        var poseViews = posesDto.Select(p =>new AllPosesViewModel(p)).ToList();
        //map DTO to AllPosesViewModel
        //return View(model)
        return View(poseViews);
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateNewPoseGet()
    {
        var categories = await _getAllCategories.ExecuteGetAllCategoriesAsync();
        var difficulties = await _getAllDifficulties.ExecuteGetAllDifficultiesAsync();
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
        _logger.LogInformation("The model is valid");

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
            return RedirectToAction("Index");
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
        var difficulties = await _getAllDifficulties.ExecuteGetAllDifficultiesAsync();
        pose.DifficultyOptions = difficulties.Select(d => new SelectListItem
        {
            Value = d.Difficulty_Id.ToString(),
            Text = d.Difficulty_Level
        }).ToList();

        var categories = await _getAllCategories.ExecuteGetAllCategoriesAsync();
        pose.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.Category_Id.ToString(),
            Text = c.Category_Name
        }).ToList();
    }
}