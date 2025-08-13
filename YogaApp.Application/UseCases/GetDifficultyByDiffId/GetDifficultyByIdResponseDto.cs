using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public class GetDifficultyByIdResponseDto
{
    public DifficultyLinkDto DifficultyLinkDto { get; set; } = new DifficultyLinkDto();
    public List<PoseLinkDto> PoseLinks { get; set; }

    public GetDifficultyByIdResponseDto(int DiffId, string difficultyName, List<PoseLinkDto> poseLinks)
    {
        DifficultyLinkDto.DifficultyId = DiffId;
        DifficultyLinkDto.DifficultyName = difficultyName;
        if (poseLinks != null && poseLinks.Any())
            PoseLinks = poseLinks;
    }
}