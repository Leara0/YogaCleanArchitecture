using System.Data;
using Dapper;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Domain.Entities;
using YogaApp.Infrastructure.DTO;

namespace YogaApp.Infrastructure.Repositories;

public class CategoryRepository:ICategoryRepository
{
    private readonly IDbConnection _db;

    public CategoryRepository(IDbConnection db)
    {
        _db = db;
    }
    
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var cats = await _db.QueryAsync<CategoryDto>("SELECT * FROM categories");
        return cats.Select(MapDtoToEntity).ToList();
    }

    public async Task<List<int>> GetCategoryIdsByPoseIdAsync(int poseId)
    {
        var cat =  await _db.QueryAsync<int>("SELECT Category_Id FROM pose_mapping WHERE pose_Id = @poseId", new { poseId });
        return cat.ToList();
    }

    public async Task<Category> GetCategoriesByCatIdAsync(int catId)
    {
        var cat = await _db.QuerySingleOrDefaultAsync<CategoryDto>("SELECT * FROM categories WHERE Category_Id = @catId", new { catId });
        return MapDtoToEntity(cat);
    }

    public async Task<List<(int CatId, string CatName)>> GetCatsInPoseAsync(List<int> catIds)
    {
        return (await _db.QueryAsync<(int CatId, string CatName)>
        ("SELECT Category_Id AS CatId, Category_Name AS CatName FROM categories WHERE category_Id IN @CatIds",
            new { CatIds = catIds })).ToList();
    }

    public async Task DeleteCategoriesByPoseIdAsync(int poseId)
    {
        await _db.ExecuteAsync("DELETE from pose_mapping WHERE pose_Id = @poseId", new { poseId });
    }

    public async Task AddCategoryByPoseIdAsync(int poseId, List<int> categoryIds)
    {
        foreach (var catId in categoryIds)
        {
            await _db.ExecuteAsync("INSERT INTO pose_mapping (Pose_Id, Category_Id) VALUES (@poseId, @categoryId)", 
                new { poseId = poseId, categoryId = catId });
        }
    }

    private Category MapDtoToEntity(CategoryDto dto)
    {
        var category = new Category();
        category.CategoryId = dto.Category_Id;
        category.CategoryName = dto.Category_Name;
        category.CategoryDescription = dto.Category_Description;
        return category;
    }

}