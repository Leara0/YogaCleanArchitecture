using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Extensions;
using YogaApp.Web.Models;

namespace YogaApp.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IApplicationServices _applicationServices;

    public CategoryController(ILogger<CategoryController> logger, IApplicationServices applicationServices)
    {
        _logger = logger;
        _applicationServices = applicationServices;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var categoriesDto = await _applicationServices.GetAllCategoriesAsync();

        //use extension method to map dto to view model
        var model = categoriesDto
            .Select(c => c.ToAllCategoriesViewModel()).ToList();
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var categoriesDto = await _applicationServices.GetCatByCatIdAsync(id);
        var model = categoriesDto.ToAllPosesViewModel();
        return View(model);
    }
}