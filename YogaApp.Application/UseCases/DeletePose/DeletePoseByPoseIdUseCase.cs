using YogaApp.Application.RespositoryInterfaces;

namespace YogaApp.Application.UseCases.DeletePose;

public class DeletePoseByPoseIdUseCase:IDeletePoseByPoseIdUseCase
{
    //set up DI
    private readonly IPoseRepository _poseRepo;
    private readonly ICategoryRepository _catRepo;

    public DeletePoseByPoseIdUseCase(IPoseRepository poseRepo, ICategoryRepository catRepo)
    {
        _poseRepo = poseRepo;
        _catRepo = catRepo;
    }
    public async Task ExecuteDeletePoseByPoseIdAsync(int poseId)
    {
        await _catRepo.DeleteCategoriesByPoseIdAsync(poseId);
        await _poseRepo.DeletePoseByPoseIdAsync(poseId);
    }
}