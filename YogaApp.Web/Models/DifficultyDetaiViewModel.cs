using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;

namespace YogaApp.Web.Models;

public class DifficultyDetaiViewModel
{
    public DifficultyLinkDto DiffLinkDto { get; set; }
    public List<PoseLinkDto> PoseLinks { get; set; }

    public DifficultyDetaiViewModel(GetDifficultyByIdResponseDto diff)
    {
        DiffLinkDto = diff.DifficultyLinkDto;
        PoseLinks = diff.PoseLinks;
    }
}