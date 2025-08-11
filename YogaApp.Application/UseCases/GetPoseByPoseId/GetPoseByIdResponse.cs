using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetPoseByIdResponse
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    
    public string? ImageUrl { get; set; }
    public DifficultyLink? DifficultyLink { get; set; } = new DifficultyLink();
    public List<CategoryLink>? CategoryLink { get; set; }

    public GetPoseByIdResponse(Pose pose, string? difficultyLevel, List<CategoryLink>? categoryLink)
    {
        PoseId = pose.PoseId;
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        TranslationOfName = pose.TranslationOfName;
        PoseDescription = pose.PoseDescription;
        PoseBenefits = pose.PoseBenefits;
        ImageUrl = pose.UrlSvg;
        DifficultyLink.DifficultyId = pose.DifficultyId;
        DifficultyLink.DifficultyName = difficultyLevel;
        CategoryLink = categoryLink;
    }
    
}