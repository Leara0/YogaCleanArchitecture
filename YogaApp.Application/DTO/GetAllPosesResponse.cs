using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllPosesResponse
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? PoseDescription { get; set; }
    public string? ThumbnailUrlSvg { get; set; }

    public GetAllPosesResponse(Pose pose)
    {
        PoseId = pose.PoseId;
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        PoseDescription = pose.PoseDescription;
        ThumbnailUrlSvg = pose.ThumbnailUrlSvg;
    }
}