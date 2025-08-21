using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Web.Models;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Extensions;

public static class DifficultyMappingExtensions
{
    public static DifficultyDetailViewModel ToDifficultyViewModel(this GetDifficultyByIdResponseDto dto)
    {
        return new DifficultyDetailViewModel
        {
            DiffLinkDto = dto.DifficultyLinkDto,
            PoseLinks = dto.PoseLinks.Select(p=> new PoseLinkViewModel()
            {
                PoseId = p.PoseId,
                PoseName = p.PoseName,
                ThumbnailSvg = p.ThumbnailSvg,
                ThumbnailLocalPath = p.ThumbnailLocalPath
            }).ToList()
        };
    }
}