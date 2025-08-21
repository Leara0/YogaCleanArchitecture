using YogaApp.Application.DTO;
using YogaApp.Web.Models;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Extensions;

public static class CategoryMappingExtensions
{
    public static AllCategoriesViewModel ToAllCategoriesViewModel(this GetAllCategoriesResponseDto dto)
    {
        return new AllCategoriesViewModel
        {
            CategoryId = dto.CategoryId,
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription
        };
    }
    public static CategoryDetailViewModel ToAllPosesViewModel(this GetCatByCatIdResponseDto dto)
    {
        return new CategoryDetailViewModel
        {
            CategoryId = dto.CategoryId,
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription,
            PoseLink = dto.PoseLink.Select(p => new PoseLinkViewModel
            {
                PoseId = p.PoseId,
                PoseName = p.PoseName,
                ThumbnailSvg = p.ThumbnailSvg,
                ThumbnailLocalPath = p.ThumbnailLocalPath
            }).ToList()
        };
    }
}