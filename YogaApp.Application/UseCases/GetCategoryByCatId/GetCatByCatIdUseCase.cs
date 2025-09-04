using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;

namespace YogaApp.Application.UseCases;

public class GetCatByCatIdUseCase : IGetCatByCatIdUseCase
{
    public readonly ICategoryRepository _catRepo;
    public readonly IPoseSearchService SearchService;

    public GetCatByCatIdUseCase(ICategoryRepository categoryRepository, IPoseSearchService searchService)
    {
        _catRepo = categoryRepository;
        SearchService = searchService;
    }
    public async Task<GetCatByCatIdResponseDto> ExecuteGetCatByCatIdAsync(int CatId)
    {
        //get all info on category
        var category = await _catRepo.GetCategoryByCatIdAsync(CatId);
        
        //get all poses that fall in this category and tuple that matches poseId and Name
        var poseIdsInCat = await SearchService.GetPoseIdsByCategoryIdAsync(CatId);
        var posesInCat = await SearchService.GetPosesByPoseIdsAsync(poseIdsInCat);
        
        //map Pose to PoseLink class for clean data handling
        var links = posesInCat.Select(p => new PoseLinkDto 
            { 
                PoseId = p.PoseId, 
                PoseName = p.PoseName, 
                ThumbnailSvg = p.ThumbnailUrlSvg,
                ThumbnailLocalPath = p.ThumbnailLocalPath
            }).ToList();
        
        return new GetCatByCatIdResponseDto(category, links);
    }
}