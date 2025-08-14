using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class CreatePoseUseCase : ICreatePoseUseCase
{
    private readonly IPoseRepository _repo;

    public CreatePoseUseCase(IPoseRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> ExecuteCreatePoseInDbAsync(CreatePoseRequestDto requestDto)
    {
        //map DTO to Entity with business validation
        var pose = new Pose(requestDto.PoseName);
        pose.SetProperties(
            requestDto.SanskritName,
            requestDto.TranslationOfName,
            requestDto.PoseDescription,
            requestDto.PoseBenefits,
            requestDto.DifficultyId,
            requestDto.UrlSvg,
            requestDto.ThumbnailUrlSvg);
        pose.CategoryIds = requestDto.CategoryIds;
        
        //Repository saves the result
        var poseId = await _repo.CreatePoseAsync(pose);
        await _repo.SavePoseCategoryAsync(poseId, pose.CategoryIds);
        return pose.PoseId;
    }
}