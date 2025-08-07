using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetAllDifficultiesUseCase
{
    Task<List<Difficulty>> ExecuteGetAllDifficultiesAsync();
}