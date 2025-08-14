using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Domain.Entities;

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

    public async Task<UpdatePoseResponseDto> ExecuteUpdatePoseAsync(int PoseId)
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
        


        return new UpdatePoseResponseDto(pose, categories, difficultyOptions, categoryOptions);
    }

    public async Task ExecuteUpdatePoseToDbAsync(UpdatePoseRequestToDbDto dto)
    {
        //map to pose entity
        var pose = new Pose(dto.PoseName, dto.DifficultyId);
        pose.SetProperties(
            dto.SanskritName,
            dto.TranslationOfName,
            dto.PoseDescription,
            dto.PoseBenefits,
            dto.UrlSvg,
            dto.ThumbnailUrlSvg);
        pose.PoseId = dto.PoseId;
        pose.CategoryIds = dto.CategoryIds;
        
        
        //call pose repo to update pose
        await _poseRepo.UpdateToDbPoseAsync(pose);
        
        //call cat repo to delete category rows so we can start fresh
        await _catRepo.DeleteCategoriesByPoseIdAsync(pose.PoseId);
        
        //call cat repo to write new category rows
        if(pose.CategoryIds?.Any() == true)
            await _catRepo.AddCategoryByPoseIdAsync(pose.PoseId, pose.CategoryIds);
    }

    
}