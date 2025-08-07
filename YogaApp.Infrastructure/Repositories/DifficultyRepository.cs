using System.Data;
using Dapper;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;
using YogaApp.Infrastructure.DTO;

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
        var dtos = await _db.QueryAsync<DifficultyDto>("SELECT * FROM difficulty");
        
        return dtos.Select(MapDtoToEntity).ToList();
    }

    public async Task<string> GetDifficultyNameByDifficultyIdAsync(int Id)
    {
        return await _db.QuerySingleOrDefaultAsync<string>
            ("SELECT difficulty_level FROM difficulty WHERE difficulty_id = @ Id", 
                new { Id });
    }
    
    private Difficulty MapDtoToEntity(DifficultyDto dto)
    {
        var difficulty = new Difficulty();
        difficulty.DifficultyId = dto.Difficulty_Id;
        difficulty.DifficultyLevel = dto.Difficulty_Level;
        return difficulty;
    }
} 