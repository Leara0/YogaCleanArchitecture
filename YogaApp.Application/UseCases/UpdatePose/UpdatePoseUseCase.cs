using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;

namespace YogaApp.Application.UseCases.UpdatePose;

public class UpdatePoseUseCase : IUpdatePoseUseCase
{
    //set up repository DI
    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _catRepo;
    private readonly IDifficultyRepository _diffRepo;

    public UpdatePoseUseCase(ICategoryRepository catRepo, IPoseRepository poseRepo,
        IDifficultyRepository diffRepo)
    {
        _catRepo = catRepo;
        _poseRepo = poseRepo;
        _diffRepo = diffRepo;
    }

    public async Task<UpdatePoseRequestDto> ExecuteUpdatePoseAsync(int PoseId)
    {
        var pose = await _poseRepo.GetPoseByIdAsync(PoseId);
        var categories = await _catRepo.GetCategoryIdsByPoseIdAsync(PoseId);
        
        //map out the SelectOptionsDto to prep for the SelectListOptions
        var difficulties = await _diffRepo.GetAllDifficultiesAsync();
        var difficultyOptions = difficulties.Select(d => new SelectOptionDto
        {
            Value = d.DifficultyId.ToString(),
            Text = d.DifficultyLevel,
        }).ToList();
        
        var categoriesOpt = await _catRepo.GetAllCategoriesAsync();
        var categoryOptions = categoriesOpt.Select(c => new SelectOptionDto
        {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName,
        }).ToList();
        


        return new UpdatePoseRequestDto(pose, categories, difficultyOptions, categoryOptions);
    }
}