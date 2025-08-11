using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetPoseByIdUseCase
{
    Task<GetPoseByIdResponse> ExecuteGetPoseByIdAsync(int PoseId);
}