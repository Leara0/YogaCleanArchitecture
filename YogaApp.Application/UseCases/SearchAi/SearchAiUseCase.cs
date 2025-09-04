using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases.SearchAi;

public class SearchAiUseCase : ISearchAiUseCase
{
    private readonly IAIYogaService _aiService;
    private readonly IPoseSearchService _searchService;

    public SearchAiUseCase(IAIYogaService aiService, IPoseSearchService searchService)
    {
        _aiService = aiService;
        _searchService = searchService;
    }
    public async Task<List<PoseLinkDto>> ExecuteGetAiSuggestionsAsync(string userGoal)
    { 
        //get ai suggestions
        var suggestedPoseNames = await _aiService.GetPoseSuggestionsAsync(userGoal);
        
        //query repository for matching poses
        var foundPoses = new List<Pose>();
        foreach (var poseName in suggestedPoseNames)
        {
            var pose = await _searchService.SearchForSingleNameAsync(poseName);
            if (pose != null)
                foundPoses.Add(pose);
        }

        var foundPosesDto = foundPoses.Select(pose => new PoseLinkDto
        {
            PoseId = pose.PoseId,
            PoseName = pose.PoseName,
            ThumbnailLocalPath = pose.ThumbnailLocalPath,
            ThumbnailSvg = pose.ThumbnailUrlSvg
        }).ToList();

        return foundPosesDto;
    }

}