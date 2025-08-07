using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllCategoriesResponse
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }

    public GetAllCategoriesResponse(Category category)
    {
        CategoryId = category.CategoryId;
        CategoryName = category.CategoryName;
        CategoryDescription = category.CategoryDescription;
    }
}