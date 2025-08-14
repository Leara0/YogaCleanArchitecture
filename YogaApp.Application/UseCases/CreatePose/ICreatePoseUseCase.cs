using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface ICreatePoseUseCase
{
    Task<int> ExecuteCreatePoseInDbAsync(CreatePoseRequestDto requestDto);
}