using Microsoft.AspNetCore.Mvc;

namespace YogaApp.Web.Controllers;

public class Category : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}