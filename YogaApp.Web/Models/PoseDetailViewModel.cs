using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Models;

public class PoseDetailViewModel
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public string? ImageUrl { get; set; }
    public DifficultyLinkViewModel DifficultyLink { get; set; } 
    public List<CategoryLinkViewModel>? CategoryLink { get; set; }
}