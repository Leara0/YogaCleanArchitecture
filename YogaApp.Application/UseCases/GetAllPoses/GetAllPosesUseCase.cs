using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases.GetAllPoses;

namespace YogaApp.Application.UseCases;

public class GetAllPosesUseCase :IGetAllPosesUseCase
{
    private readonly IPoseRepository _poseRepo;

    public GetAllPosesUseCase(IPoseRepository poseRepo)
    {
        _poseRepo = poseRepo;
    }

    public async Task<PosesByDifficultyDto> ExecuteGetAllPosesAsync()
    {
        var poses = await _poseRepo.GetAllPosesAsync();
        
        var posesDto = poses.Select(p => new GetAllPosesResponseDto(p)).ToList();
        return new PosesByDifficultyDto
        {
            EasyPoses = posesDto.Where(p => p.DifficultyId == 1).ToList(),
            MediumPoses = posesDto.Where(p => p.DifficultyId == 2).ToList(),
            HardPoses = posesDto.Where(p => p.DifficultyId == 3).ToList(),
        };
    }
    
}
