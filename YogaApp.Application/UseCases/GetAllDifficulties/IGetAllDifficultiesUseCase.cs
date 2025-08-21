using YogaApp.Application.UseCases.GetAllDifficulties;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetAllDifficultiesUseCase
{
    Task<List<GetAllDifficultiesResponseDto>> ExecuteGetAllDifficultiesAsync();
}