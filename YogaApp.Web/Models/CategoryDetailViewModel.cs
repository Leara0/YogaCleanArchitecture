using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Web.Models;

public class CategoryDetailViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<PoseLinkDto> PoseLink { get; set; }

    public CategoryDetailViewModel(GetCatByCatIdResponseDto cat)
    {
        CategoryId = cat.CategoryId;
        CategoryName = cat.CategoryName;
        CategoryDescription = cat.CategoryDescription;
        PoseLink = cat.PoseLink;
    }
}