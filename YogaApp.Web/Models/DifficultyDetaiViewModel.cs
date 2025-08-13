using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;

namespace YogaApp.Web.Models;

public class DifficultyDetaiViewModel
{
    public DifficultyLink DiffLink { get; set; }
    public List<PoseLink> PoseLinks { get; set; }

    public DifficultyDetaiViewModel(GetDifficultyByIdResponse diff)
    {
        DiffLink = diff.DifficultyLink;
        PoseLinks = diff.PoseLinks;
    }
}