using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;

namespace YogaApp.Application.UseCases.Search;

public class SearchUseCase: ISearchUseCase
{
    private readonly IPoseSearchService _searchService;
    private readonly ICategoryRepository _catRepo;

    public SearchUseCase(IPoseSearchService searchService, ICategoryRepository catRepo)
    {
        _searchService = searchService;
        _catRepo = catRepo;
    }
    public async Task<SearchResponseDto> ExecuteSearchAsync(string searchString)
    {
        //call repo to search for names
        var namePoses = await _searchService.SearchByNameAsync(searchString);
        
        //map to PoseLinkDto
        var namePoseLinks = namePoses.Select(p => new PoseLinkDto
        {
            PoseId = p.PoseId,
            PoseName = p.PoseName,
            ThumbnailLocalPath = p.ThumbnailLocalPath,
            ThumbnailSvg = p.ThumbnailUrlSvg
        }).ToList();

        //search descriptions
        var descriptionPoses = await _searchService.SearchByDescriptionAsync(searchString);
        var descPoseLinks = descriptionPoses.Select(p => new PoseLinkDto
        {
            PoseId = p.PoseId,
            PoseName = p.PoseName,
            ThumbnailLocalPath = p.ThumbnailLocalPath,
            ThumbnailSvg = p.ThumbnailUrlSvg
        }).ToList();

        //search benefits
        var benefitsPoses = await _searchService.SearchByBenefitsAsync(searchString);
        var benePoseLinks = benefitsPoses.Select(p => new PoseLinkDto
        {
            PoseId = p.PoseId,
            PoseName = p.PoseName,
            ThumbnailLocalPath = p.ThumbnailLocalPath,
            ThumbnailSvg = p.ThumbnailUrlSvg
        }).ToList();
        
        //search categories
        var categories = await _catRepo.SearchByCategoryAsync(searchString);
        var categoryLinks = categories.Select(c => new CategoryLinkDto
        {
            CategoryId = c.CatId,
            CategoryName = c.CatName,
        }).ToList();


        return new SearchResponseDto(namePoseLinks, descPoseLinks, benePoseLinks, categoryLinks);
    }
}