using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;

namespace YogaApp.Web.Models;

public class DifficultyDetailViewModel
{
    public DifficultyLinkDto DiffLinkDto { get; set; }
    public List<PoseLinkDto> PoseLinks { get; set; }

}