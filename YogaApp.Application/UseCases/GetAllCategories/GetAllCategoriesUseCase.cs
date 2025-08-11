using YogaApp.Application.DTO;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<GetAllCategoriesResponse>> ExecuteGetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return categories.Select(c => new GetAllCategoriesResponse(c)).ToList();
    }
}