using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class CreatePoseInDbUseCase : ICreatePoseInDbUseCase
{
    private readonly IPoseRepository _repo;

    public CreatePoseInDbUseCase(IPoseRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> ExecuteCreatePoseAsync(CreatePoseRequest request)
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
            request.ThumbnailUrlSvg);
        pose.CategoryIds = request.CategoryIds;
        
        //Repository saves the result
        var poseId = await _repo.CreatePoseAsync(pose);
        await _repo.SavePoseCategoryAsync(poseId, pose.CategoryIds);
        return pose.PoseId;
    }
}