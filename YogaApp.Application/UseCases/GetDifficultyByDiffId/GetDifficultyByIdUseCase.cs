using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;

namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public class GetDifficultyByIdUseCase : IGetDifficultyByIdUseCase
{
    private readonly IDifficultyRepository _diffRepo;
    private readonly IPoseRepository _poseRepo;

    public GetDifficultyByIdUseCase(IDifficultyRepository diffRepo, IPoseRepository poseRepo)
    {
        _diffRepo = diffRepo;
        _poseRepo = poseRepo;
    }
    public async Task<GetDifficultyByIdResponseDto> ExecuteGetDifficultyById(int DiffId)
    {
        // call difficulty repo to get name of difficulty from diff id (if difficulty not null)
       string difficultyLevel = await _diffRepo.GetDifficultyNameByDifficultyIdAsync(DiffId);
       
       //get all poses that fall in this difficulty and tuple that matches poseId and Name
       var poseIdsInDiff = await _poseRepo.GetPoseIdsByDifficultyIdAsync(DiffId);
       var posesInCat = await _poseRepo.GetPoseLinkByPoseIdAsync(poseIdsInDiff);
        
       //map tuple to PoseLink class for easier data handling
       var links = posesInCat.Select(p => 
           new PoseLinkDto { PoseId = p.PoseId, PoseName = p.PoseName }).ToList();
       
       return new GetDifficultyByIdResponseDto(DiffId, difficultyLevel, links);
    }
}