using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Web.Models;

public class PoseDetailsViewModel
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

    public PoseDetailsViewModel(GetPoseByIdResponse pose)
    {
        PoseId = pose.PoseId;
        PoseName = pose.PoseName;
        SanskritName = pose.SanskritName;
        TranslationOfName = pose.TranslationOfName;
        PoseDescription = pose.PoseDescription;
        PoseBenefits = pose.PoseBenefits;
        ImageUrl = pose.ImageUrl;
        DifficultyLink = pose.DifficultyLink;
        CategoryLink = pose.CategoryLink;
    }
}