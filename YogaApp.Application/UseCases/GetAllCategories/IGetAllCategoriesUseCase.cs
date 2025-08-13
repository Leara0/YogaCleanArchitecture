using YogaApp.Application.DTO;
using YogaApp.Application.UseCases;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetAllCategoriesUseCase
{
    Task<List<GetAllCategoriesResponseDto>> ExecuteGetAllCategoriesAsync();
}