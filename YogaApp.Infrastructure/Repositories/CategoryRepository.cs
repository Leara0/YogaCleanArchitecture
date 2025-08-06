using System.Data;
using Dapper;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

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
        var cat = await _db.QueryAsync<Category>("SELECT * FROM categories");
        return cat.ToList();
    }

    public async Task<List<int>> GetCategoryIdsByPoseIdAsync(int poseId)
    {
        var cat =  await _db.QueryAsync<int>("SELECT Category_Id FROM pose_mapping WHERE pose_Id = @poseId", new { poseId });
        return cat.ToList();
    }

}