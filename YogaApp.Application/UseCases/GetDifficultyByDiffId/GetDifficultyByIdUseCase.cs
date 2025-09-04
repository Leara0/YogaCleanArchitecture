using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;

namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public class GetDifficultyByIdUseCase : IGetDifficultyByIdUseCase
{
    private readonly IPoseSearchService _searchService;

    public GetDifficultyByIdUseCase(IPoseSearchService searchService)
    {
        _searchService = searchService;
    }
    public async Task<GetDifficultyByIdResponseDto> ExecuteGetDifficultyById(int DiffId)
    {
       // uses switch to get the name of the diff level from the id
       string difficultyLevel = DiffId switch
       {
           1 => "Beginner",
           2 => "Intermediate",
           3 => "Advanced",
       };
       
       //get id for poses that fall in this category and then complete Pose info for those poses
       var poseIdsInDiff = await _searchService.GetPoseIdsByDifficultyIdAsync(DiffId);
       var posesInCat = await _searchService.GetPosesByPoseIdsAsync(poseIdsInDiff);
        
       //map Pose to PoseLink class for easier data handling
       var links = posesInCat.Select(p => new PoseLinkDto
       {
           PoseId = p.PoseId, 
           PoseName = p.PoseName, 
           ThumbnailSvg = p.ThumbnailUrlSvg,
           ThumbnailLocalPath = p.ThumbnailLocalPath
       }).ToList();
       
       return new GetDifficultyByIdResponseDto(DiffId, difficultyLevel, links);
    }
}