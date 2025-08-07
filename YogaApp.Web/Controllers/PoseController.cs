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
    private readonly GetPoseByIdUseCase _getPoseByIdUseCase;

    public PoseController(ILogger<PoseController> logger, GetAllCategoriesUseCase getAllCategories,
        GetAllDifficultiesUseCase getAllDifficulties, CreatePoseUseCase createPoseUseCase, GetAllPosesUseCase getAllPosesUseCase,
        GetPoseByIdUseCase getPoseByIdUseCase)
    {
        _logger = logger;
        
        _getAllCategories = getAllCategories;
        _getAllDifficulties = getAllDifficulties;
        _createPoseUseCase = createPoseUseCase;
        _getAllPosesUseCase = getAllPosesUseCase;
        _getPoseByIdUseCase = getPoseByIdUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //get list of GetAllPosesResponse DTOs
        var posesDto = await _getAllPosesUseCase.ExecuteGetAllPosesAsync();
        //map onto View Model
        var poseViews = posesDto.Select(p =>new AllPosesViewModel(p)).ToList();
        //return View(model)
        return View(poseViews);
    }

    public async Task<IActionResult> Detail(int id)
    {
        //get GetPoseByIdResponse DTO
        var poseDto = await _getPoseByIdUseCase.ExecuteGetPoseByIdAsync(id);
        // map onto View Model
        
        return View(poseDto);
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
            Value = d.DifficultyId.ToString(),
            Text = d.DifficultyLevel
        }).ToList();

        var categories = await _getAllCategories.ExecuteGetAllCategoriesAsync();
        pose.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName
        }).ToList();
    }
}