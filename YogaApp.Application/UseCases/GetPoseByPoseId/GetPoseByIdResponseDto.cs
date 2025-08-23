using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetPoseByIdResponseDto
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    
    public string? ImageUrl { get; set; }
    public DifficultyLinkDto? DifficultyLink { get; set; } = new DifficultyLinkDto();
    public List<CategoryLinkDto>? CategoryLink { get; set; }

    public GetPoseByIdResponseDto(Pose pose, string? difficultyLevel, List<CategoryLinkDto>? categoryLink)
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
    //parameterless constructor for use in testing
    public GetPoseByIdResponseDto()
    {}

}