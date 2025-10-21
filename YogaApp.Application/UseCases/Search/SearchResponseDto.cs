using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.Search;

public class SearchResponseDto
{
    public List<PoseLinkDto> NameLinks { get; set; } = new List<PoseLinkDto>();
    public List<PoseLinkDto> DescriptionLinks { get; set; } = new List<PoseLinkDto>();
    public List<PoseLinkDto> BenefitsLinks { get; set; } = new List<PoseLinkDto>();
    public List<CategoryLinkDto> CategoriesLinks { get; set; } = new List<CategoryLinkDto>();
    
    
    public SearchResponseDto(List<PoseLinkDto> nameLinks, List<PoseLinkDto> descLinks, List<PoseLinkDto> beneLinks,
        List<CategoryLinkDto> catLinks)
    {
        NameLinks = nameLinks;
        DescriptionLinks = descLinks;
        BenefitsLinks = beneLinks;
        CategoriesLinks = catLinks;
    }
}