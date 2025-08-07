using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases;

public class GetAllCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<Category>> ExecuteGetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return categories.ToList();
    }
}