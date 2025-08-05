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
}