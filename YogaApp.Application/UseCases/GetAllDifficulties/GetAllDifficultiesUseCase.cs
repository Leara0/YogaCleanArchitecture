using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetAllDifficultiesUseCase: IGetAllDifficultiesUseCase
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