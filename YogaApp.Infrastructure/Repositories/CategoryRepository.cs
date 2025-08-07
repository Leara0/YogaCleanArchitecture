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
        var cat = await _db.QueryAsync<CategoryDto>("SELECT * FROM categories");
        return cat.Select(MapDtoToEntity).ToList();
    }

    public async Task<List<int>> GetCategoryIdsByPoseIdAsync(int poseId)
    {
        var cat =  await _db.QueryAsync<int>("SELECT Category_Id FROM pose_mapping WHERE pose_Id = @poseId", new { poseId });
        return cat.ToList();
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