using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetAllPosesUseCase
{
    Task<List<GetAllPosesResponse>> ExecuteGetAllPosesAsync();
}