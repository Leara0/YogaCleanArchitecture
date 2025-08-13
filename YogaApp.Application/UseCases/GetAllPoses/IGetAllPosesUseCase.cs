using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllPoses;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IGetAllPosesUseCase
{
    Task<PosesByDifficultyDto> ExecuteGetAllPosesAsync();
}