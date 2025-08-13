using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.GetAllPoses;

public class PosesByDifficultyDto
{
    public List<GetAllPosesResponseDto> EasyPoses { get; set; } = new List<GetAllPosesResponseDto>();
    public List<GetAllPosesResponseDto> MediumPoses { get; set; } = new List<GetAllPosesResponseDto>();
    public List<GetAllPosesResponseDto> HardPoses { get; set; } = new List<GetAllPosesResponseDto>();
}