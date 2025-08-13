using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases.UpdatePose;

public class UpdatePoseRequest
{
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int? DifficultyId { get; set; }
    public string? UrlSvg { get; set; }
    public string? ThumbnailUrlSvg { get; set; }
    public List<int>? CategoryIds { get; set; } = new List<int>();

    public UpdatePoseRequest(Pose pose, List<int> categories)
    {
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        TranslationOfName = pose.TranslationOfName;
        PoseDescription = pose.PoseDescription;
        PoseBenefits = pose.PoseBenefits;
        DifficultyId = pose.DifficultyId;
        UrlSvg = pose.UrlSvg;
        ThumbnailUrlSvg = pose.ThumbnailUrlSvg;
        CategoryIds = categories;
    }
}