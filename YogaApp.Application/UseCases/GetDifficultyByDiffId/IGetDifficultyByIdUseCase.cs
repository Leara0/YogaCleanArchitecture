namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public interface IGetDifficultyByIdUseCase
{
    public Task<GetDifficultyByIdResponseDto> ExecuteGetDifficultyById(int DiffId);
}