using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public class GetDifficultyByIdResponse
{
    public DifficultyLink DifficultyLink { get; set; } = new DifficultyLink();
    public List<PoseLink> PoseLinks { get; set; }

    public GetDifficultyByIdResponse(int DiffId, string difficultyName, List<PoseLink> poseLinks)
    {
        DifficultyLink.DifficultyId = DiffId;
        DifficultyLink.DifficultyName = difficultyName;
        if (poseLinks != null && poseLinks.Any())
            PoseLinks = poseLinks;
    }
}