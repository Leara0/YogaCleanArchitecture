using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.SearchAi;

public class SearchAiResponseDto
{
    public List<PoseLinkDto> PoseLinks { get; set; } = new List<PoseLinkDto>();
    
    public SearchAiResponseDto(List<PoseLinkDto> poseLinks)
    {
        PoseLinks = poseLinks;
    }
}