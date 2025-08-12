using Microsoft.AspNetCore.Mvc;

namespace YogaApp.Web.Controllers;

public class DifficultyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}