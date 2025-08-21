using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Models;

public class CategoryDetailViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<PoseLinkViewModel> PoseLink { get; set; }

}