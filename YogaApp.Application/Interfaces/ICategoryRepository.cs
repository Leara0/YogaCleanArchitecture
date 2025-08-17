using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<int>> GetCategoryIdsByPoseIdAsync(int poseId);
    Task<Category> GetCategoriesByCatIdAsync(int catId);
    Task<List<(int CatId, string CatName)>> GetCatsInPoseAsync(List<int> catIds);
    Task DeleteCategoriesByPoseIdAsync(int poseId);
    Task AddCategoryByPoseIdAsync(int poseId, List<int> categoryIds);//assists in creating a new pose by adding to pose mapping
}