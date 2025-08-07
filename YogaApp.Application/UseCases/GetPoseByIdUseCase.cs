using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetPoseByIdUseCase
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

    public async Task<GetPoseByIdResponse> ExecuteGetPoseByIdAsync(int PoseId)
    {
        //call repo to get pose entity by Id
        var pose = await _poseRepo.GetPoseByIdAsync(PoseId);
        
        //call catrepo to get List<catIds> and Category Entities
        var categoryIds = await _catRepo.GetCategoryIdsByPoseIdAsync(PoseId);
        var categories = await _catRepo.GetAllCategoriesAsync();
        
        // use join statement to Make a list of CategoryLink 
        var categoryLinks = categoryIds
            .Join(categories,
                id => id,
                c => c.CategoryId,
                (id, c) => new CategoryLink { CategoryId = id, CategoryName = c.CategoryName })
            .ToList();
        
        // call difficulty repo to get name of difficulty from diff id (if difficulty not null)
        string difficultyLevel = null;
        if (pose.DifficultyId != null)
        {
            difficultyLevel = await _diffRepo.GetDifficultyNameByDifficultyIdAsync(pose.DifficultyId.Value);
        }

        //use custom constructor to map to CetPoseByIdResponse
        return new GetPoseByIdResponse(pose, difficultyLevel, categoryLinks);
    }
    
    
}
