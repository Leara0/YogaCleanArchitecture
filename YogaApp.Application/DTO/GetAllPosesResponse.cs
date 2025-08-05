using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllPosesResponse
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int? DifficultyId { get; set; }
    public Difficulty Difficulty { get; set; }
    public List<int>? CategoryIds { get; set; }
    public string? UrlSvg { get; set; }
    public string? ThumbnailUrlSvg { get; set; }
}