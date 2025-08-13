using YogaApp.Application.DTO;

namespace YogaApp.Web.Models;

public class AllPosesViewModel
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? PoseDescription { get; set; }
    public int? Difficulty_Id { get; set; }
    public string? ThumbnailUrlSvg { get; set; }
}