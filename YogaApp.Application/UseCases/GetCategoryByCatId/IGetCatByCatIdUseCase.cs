using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetCatByCatIdUseCase
{
    Task<GetCatByCatIdResponse> ExecuteGetCatByCatIdAsync(int CatId);
}