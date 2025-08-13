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

    public async Task<UpdatePoseRequest> ExecuteUpdatePoseAsync(int PoseId)
    {
        var pose = await _poseRepo.GetPoseByIdAsync(PoseId);
        var categories = await _catRepo.GetCategoryIdsByPoseIdAsync(PoseId);


        return new UpdatePoseRequest(pose, categories);
    }
}