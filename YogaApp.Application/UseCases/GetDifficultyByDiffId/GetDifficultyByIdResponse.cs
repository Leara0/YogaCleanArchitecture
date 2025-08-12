using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public class GetDifficultyByIdResponse
{
    public DifficultyLink DifficultyLink { get; set; } = new DifficultyLink();
    public List<PoseLink> Poses { get; set; }

    public GetDifficultyByIdResponse(int DiffId, string difficultyName, List<PoseLink> poses)
    {
        DifficultyLink.DifficultyId = DiffId;
        DifficultyLink.DifficultyName = difficultyName;
        Poses = poses;
    }
}