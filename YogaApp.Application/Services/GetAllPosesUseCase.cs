using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.Services;

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