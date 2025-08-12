namespace YogaApp.Application.UseCases.GetDifficultyByDiffId;

public interface IGetDifficultyByIdUseCase
{
    public Task<GetDifficultyByIdResponse> ExecuteGetDifficultyById(int DiffId);
}