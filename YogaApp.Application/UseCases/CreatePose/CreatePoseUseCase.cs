using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class CreatePoseUseCase : ICreatePoseUseCase
{
    private readonly IPoseRepository _repo;
    private readonly ICategoryRepository _catRepo;

    public CreatePoseUseCase(IPoseRepository repo, ICategoryRepository catRepo)
    {
        _repo = repo;
        _catRepo = catRepo;
    }

    public async Task<int> ExecuteCreatePoseInDbAsync(CreatePoseRequestDto requestDto)
    {
        //map DTO to Entity with business validation
        var pose = new Pose(requestDto.PoseName, requestDto.DifficultyId);
        pose.SetProperties(
            requestDto.SanskritName,
            requestDto.TranslationOfName,
            requestDto.PoseDescription,
            requestDto.PoseBenefits,
            requestDto.UrlSvg,
            requestDto.ThumbnailUrlSvg);
        pose.CategoryIds = requestDto.CategoryIds;
        
        //Repository saves the result
        var poseId = await _repo.CreatePoseAsync(pose);
        if(pose.CategoryIds != null && pose.CategoryIds.Any())
            await _catRepo.AddCategoryByPoseIdAsync(poseId, pose.CategoryIds);
        
        return poseId;
    }
}