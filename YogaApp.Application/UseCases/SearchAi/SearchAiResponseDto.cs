using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.SearchAi;

public class SearchAiResponseDto
{
    public SearchAiResponseDto(List<PoseLinkDto> poseLinks)
    {
        PoseLinks = poseLinks;
    }
    
    public List<PoseLinkDto> PoseLinks { get; set; } = new List<PoseLinkDto>();
}