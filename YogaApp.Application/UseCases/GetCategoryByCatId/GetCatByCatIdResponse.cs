using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetCatByCatIdResponse
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<PoseLink> PoseLink { get; set; }

    public GetCatByCatIdResponse(Category category, List<PoseLink> poseLink)
    {
        CategoryId = category.CategoryId;
        CategoryName = category.CategoryName;
        CategoryDescription = category.CategoryDescription;
        if (poseLink != null && poseLink.Any())
            PoseLink = poseLink;
    }
}