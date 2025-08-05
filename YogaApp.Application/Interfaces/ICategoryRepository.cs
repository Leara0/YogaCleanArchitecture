using YogaApp.Domain.Entities;

namespace YogaApp.Application.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
}