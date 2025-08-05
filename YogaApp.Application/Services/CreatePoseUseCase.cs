using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.Services;

public class CreatePoseUseCase
{
    private readonly IPoseRepository _repo;

    public CreatePoseUseCase(IPoseRepository repo)
    {
        _repo = repo;
    }

    public async Task<Pose> ExecuteCreatePoseAsync(CreatePoseRequest request)
    {
        //map DTO to Entity with business validation
        var pose = new Pose(request.PoseName);
        pose.SetProperties(
            request.SanskritName,
            request.TranslationOfName,
            request.PoseDescription,
            request.PoseBenefits,
            request.DifficultyId,
            request.UrlSvg,
            request.ThumbnailUrlSvg,
            request.CategoryIds);
        
        
        //Repository saves the result
        var poseId = await _repo.CreatePoseAsync(pose);
        await _repo.SavePoseCategoryAsync(poseId, pose.CategoryIds);
        return pose;
    }
}