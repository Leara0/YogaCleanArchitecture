using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetCatByCatIdResponseDto
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<PoseLinkDto> PoseLink { get; set; }

    public GetCatByCatIdResponseDto(Category category, List<PoseLinkDto> poseLink)
    {
        CategoryId = category.CategoryId;
        CategoryName = category.CategoryName;
        CategoryDescription = category.CategoryDescription;
        if (poseLink != null && poseLink.Any())
            PoseLink = poseLink;
    }
}