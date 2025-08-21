using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllPosesResponseDto
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? PoseDescription { get; set; }
    public int DifficultyId { get; set; }
    public string? ThumbnailUrlSvg { get; set; }
    public string? ThumbnailLocalPath { get; set; }

    public GetAllPosesResponseDto(Pose pose)
    {
        PoseId = pose.PoseId;
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        PoseDescription = pose.PoseDescription;
        DifficultyId = pose.DifficultyId;
        ThumbnailUrlSvg = pose.ThumbnailUrlSvg;
        ThumbnailLocalPath = pose.ThumbnailLocalPath;
    }
}