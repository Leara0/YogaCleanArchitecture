using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetCatByCatIdUseCase
{
    Task<GetCatByCatIdResponseDto> ExecuteGetCatByCatIdAsync(int CatId);
}