using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.Search;

public class SearchResponseDto
{
    public List<PoseLinkDto> NameLinks { get; set; }
    public List<PoseLinkDto> DescriptionLinks { get; set; }
    public List<PoseLinkDto> BenefitsLinks { get; set; }
    public List<CategoryLinkDto> CategoriesLinks { get; set; }
}