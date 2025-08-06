using YogaApp.Domain.Entities;

namespace YogaApp.Application.Interfaces;

public interface IDifficultyRepository
{
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    
    Task<string> GetDifficultyNameByDifficultyIdAsync(int Id);
}