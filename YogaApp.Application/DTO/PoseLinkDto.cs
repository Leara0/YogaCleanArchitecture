namespace YogaApp.Application.DTO;

public class PoseLinkDto
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? ThumbnailSvg {get; set;}
    public string? ThumbnailLocalPath { get; set; }
    
}