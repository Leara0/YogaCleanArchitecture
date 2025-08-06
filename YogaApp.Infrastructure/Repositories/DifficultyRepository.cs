using System.Data;
using Dapper;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Infrastructure.Repositories;

public class DifficultyRepository:IDifficultyRepository
{
    private readonly IDbConnection _db;

    public DifficultyRepository(IDbConnection db)
    {
        _db = db;
    }
    
    public async Task<List<Difficulty>> GetAllDifficultiesAsync()
    {
        var diff = await _db.QueryAsync<Difficulty>("SELECT * FROM difficulty");
        return diff.ToList();
    }

    public async Task<string> GetDifficultyNameByDifficultyIdAsync(int Id)
    {
        return await _db.QuerySingleOrDefaultAsync<string>
            ("SELECT difficulty_level FROM difficulty WHERE difficulty_id = @ Id", 
                new { Id });
    }
} 