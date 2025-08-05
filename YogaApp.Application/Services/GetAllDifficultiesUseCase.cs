using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.Services;

public class GetAllDifficultiesUseCase
{
    private readonly IDifficultyRepository _difficultyRepository;

    public GetAllDifficultiesUseCase(IDifficultyRepository difficultyRepository)
    {
        _difficultyRepository = difficultyRepository;
    }
    
    public async Task<List<Difficulty>> ExecuteGetAllDifficultiesAsync()
    {
        var difficulties = await _difficultyRepository.GetAllDifficultiesAsync();
        return difficulties.ToList();
    }
}