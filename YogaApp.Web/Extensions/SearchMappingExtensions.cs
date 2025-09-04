using YogaApp.Application.UseCases.Search;
using YogaApp.Application.UseCases.SearchAi;
using YogaApp.Web.Models;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Extensions;

public static class SearchMappingExtensions
{
    public static SearchViewModel ToSearchViewModel(this SearchResponseDto dto)
    {
        return new SearchViewModel
        {
            NameLinks = dto.NameLinks.Select(d => new PoseLinkViewModel
            {
                PoseId = d.PoseId,
                PoseName = d.PoseName,
                ThumbnailSvg = d.ThumbnailSvg,
                ThumbnailLocalPath = d.ThumbnailLocalPath
            }).ToList() ?? new List<PoseLinkViewModel>(),
            
            DescriptionLinks = dto.DescriptionLinks.Select(d => new PoseLinkViewModel
            {
                PoseId = d.PoseId,
                PoseName = d.PoseName,
                ThumbnailSvg = d.ThumbnailSvg,
                ThumbnailLocalPath = d.ThumbnailLocalPath
            }).ToList() ?? new List<PoseLinkViewModel>(),
            
            BenePoseLinks = dto.BenefitsLinks.Select(d=> new PoseLinkViewModel()
                {
                    PoseId = d.PoseId,
                    PoseName = d.PoseName,
                    ThumbnailSvg = d.ThumbnailSvg,
                    ThumbnailLocalPath = d.ThumbnailLocalPath
                }).ToList() ?? new List<PoseLinkViewModel>(),
            
            CategoryLinks = dto.CategoriesLinks.Select(d => new CategoryLinkViewModel
            {
                CategoryId = d.CategoryId,
                CategoryName = d.CategoryName,
            }).ToList() ?? new List<CategoryLinkViewModel>(),
        };
    }

    public static AiSequenceViewModel ToAiSequenceViewModel(this SearchAiResponseDto dto)
    {
        return new AiSequenceViewModel
        {
            SuggestedPoses = dto.PoseLinks.Select(pose => new PoseLinkViewModel()
            {
                PoseId = pose.PoseId,
                PoseName = pose.PoseName,
                ThumbnailLocalPath = pose.ThumbnailLocalPath,
                ThumbnailSvg = pose.ThumbnailSvg
            }).ToList()
        };
    }
}