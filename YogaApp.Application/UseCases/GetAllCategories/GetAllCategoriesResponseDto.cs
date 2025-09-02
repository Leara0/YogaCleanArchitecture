using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllCategoriesResponseDto
{
    public GetAllCategoriesResponseDto(Category category)
    {
        CategoryId = category.CategoryId;
        CategoryName = category.CategoryName;
        CategoryDescription = category.CategoryDescription;
    }
    
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
}