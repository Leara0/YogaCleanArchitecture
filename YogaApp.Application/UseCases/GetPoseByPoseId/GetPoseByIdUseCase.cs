using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetPoseByIdUseCase : IGetPoseByIdUseCase
{
    //set up repository DI
    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _catRepo;
    private readonly IDifficultyRepository _diffRepo;

    public GetPoseByIdUseCase(ICategoryRepository catRepo, IPoseRepository poseRepo,
        IDifficultyRepository diffRepo)
    {
        _catRepo = catRepo;
        _poseRepo = poseRepo;
        _diffRepo = diffRepo;
    }

    public async Task<GetPoseByIdResponseDto> ExecuteGetPoseByIdAsync(int PoseId)
    {
        //call repo to get pose entity by Id
        var pose = await _poseRepo.GetPoseByIdAsync(PoseId);
        
        //get all categories that fall in this pose and tuple that matches CatId and Name
        var categoryIds = await _catRepo.GetCategoryIdsByPoseIdAsync(PoseId);
        var catsInThisPose = await _catRepo.GetCatsInPoseAsync(categoryIds);
        
        //map tuple to PoseLink class for easier data handling
        var categoryLinks = catsInThisPose.Select(c => 
            new CategoryLinkDto() { CategoryId = c.CatId, CategoryName = c.CatName }).ToList();
        
        // call difficulty repo to get name of difficulty from diff id (if difficulty not null)
        string difficultyLevel = null;
        if (pose.DifficultyId != null)
        {
            difficultyLevel = await _diffRepo.GetDifficultyNameByDifficultyIdAsync(pose.DifficultyId.Value);
        }

        //use custom constructor to map to CetPoseByIdResponse
        return new GetPoseByIdResponseDto(pose, difficultyLevel, categoryLinks);
    }
    
    
}
