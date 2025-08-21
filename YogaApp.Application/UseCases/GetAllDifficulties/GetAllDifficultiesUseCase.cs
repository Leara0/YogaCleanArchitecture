using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases.GetAllDifficulties;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetAllDifficultiesUseCase: IGetAllDifficultiesUseCase
{
    private readonly IDifficultyRepository _difficultyRepository;

    public GetAllDifficultiesUseCase(IDifficultyRepository difficultyRepository)
    {
        _difficultyRepository = difficultyRepository;
    }
    
    public async Task<List<GetAllDifficultiesResponseDto>> ExecuteGetAllDifficultiesAsync()
    {
        var difficulties = await _difficultyRepository.GetAllDifficultiesAsync();
        var difficultyDto = difficulties.Select(d => new GetAllDifficultiesResponseDto(d)).ToList();
        return difficultyDto;
    }
}