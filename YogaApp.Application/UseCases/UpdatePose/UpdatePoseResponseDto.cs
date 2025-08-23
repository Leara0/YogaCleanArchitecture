using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases.UpdatePose;

public class UpdatePoseResponseDto
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int DifficultyId { get; set; }
    public string? UrlSvg { get; set; }
    public string? ThumbnailUrlSvg { get; set; }
    public List<int>? CategoryIds { get; set; } = new List<int>();
    
    public List<SelectOptionDto> DifficultyOptions { get; set; } = new List<SelectOptionDto>();
    public List<SelectOptionDto> CategoryOptions { get; set; } = new List<SelectOptionDto>();

    public UpdatePoseResponseDto(Pose pose, List<int> categories, 
        List<SelectOptionDto> difficultyOptions, List<SelectOptionDto> categoryOptions)
    {
        PoseId = pose.PoseId;
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        TranslationOfName = pose.TranslationOfName;
        PoseDescription = pose.PoseDescription;
        PoseBenefits = pose.PoseBenefits;
        DifficultyId = pose.DifficultyId;
        UrlSvg = pose.UrlSvg;
        ThumbnailUrlSvg = pose.ThumbnailUrlSvg;
        CategoryIds = categories;
        DifficultyOptions = difficultyOptions;
        CategoryOptions = categoryOptions;
    }
    //empty constructor for use in testing
    public UpdatePoseResponseDto()
    {}
}