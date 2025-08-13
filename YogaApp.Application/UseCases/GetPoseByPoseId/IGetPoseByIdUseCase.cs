using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetPoseByIdUseCase
{
    Task<GetPoseByIdResponseDto> ExecuteGetPoseByIdAsync(int PoseId);
}