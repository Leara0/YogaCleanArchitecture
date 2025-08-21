using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Models;

public class DifficultyDetailViewModel
{
    public DifficultyLinkDto DiffLinkDto { get; set; }
    public List<PoseLinkViewModel> PoseLinks { get; set; }

}