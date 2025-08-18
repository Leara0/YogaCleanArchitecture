using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface IDifficultyRepository
{
    Task<List<Difficulty>> GetAllDifficultiesAsync();
}
