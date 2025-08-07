using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;

namespace YogaApp.Application.UseCases;

public class GetAllPosesUseCase
{
    private readonly IPoseRepository _poseRepo;

    public GetAllPosesUseCase(IPoseRepository poseRepo)
    {
        _poseRepo = poseRepo;
    }

    public async Task<List<GetAllPosesResponse>> ExecuteGetAllPosesAsync()
    {
        var poses = await _poseRepo.GetAllPosesAsync();
        
        return poses.Select(p => new GetAllPosesResponse(p)).ToList();
    }
    
}
