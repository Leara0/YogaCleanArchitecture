using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;

namespace YogaApp.Application.UseCases;

public class GetCatByCatIdUseCase : IGetCatByCatIdUseCase
{
    public readonly ICategoryRepository _catRepo;
    public readonly IPoseRepository _poseRepo;

    public GetCatByCatIdUseCase(ICategoryRepository categoryRepository, IPoseRepository poseRepository)
    {
        _catRepo = categoryRepository;
        _poseRepo = poseRepository;
    }
    public async Task<GetCatByCatIdResponse> ExecuteGetCatByCatIdAsync(int CatId)
    {
        //get all info on category
        var category = await _catRepo.GetCategoriesByCatIdAsync(CatId);
        
        //get all poses that fall in this category and tuple that matches poseId and Name
        var poseIdsInCat = await _poseRepo.GetPoseIdsByCategoryIdAsync(CatId);
        var posesInCat = await _poseRepo.GetPosesInCategoryAsync(poseIdsInCat);
        
        //map tuple to PoseLink class for easier data handling
        var links = posesInCat.Select(p => 
            new PoseLink { PoseId = p.PoseId, PoseName = p.PoseName }).ToList();
        
        return new GetCatByCatIdResponse(category, links);
    }
}