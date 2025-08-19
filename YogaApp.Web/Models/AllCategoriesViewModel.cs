using YogaApp.Application.DTO;

namespace YogaApp.Web.Models;

public class AllCategoriesViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
}