using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Web.Models;

namespace YogaApp.Web.Extensions;

public static class DifficultyMappingExtensions
{
    public static DifficultyDetailViewModel ToDifficultyViewModel(this GetDifficultyByIdResponseDto dto)
    {
        return new DifficultyDetailViewModel
        {
            DiffLinkDto = dto.DifficultyLinkDto,
            PoseLinks = dto.PoseLinks
        };
    }
}