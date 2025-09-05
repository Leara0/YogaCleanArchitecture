using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.SearchAi;

public interface ISearchAiUseCase
{
    Task<List<PoseLinkDto>> ExecuteGetAiSuggestionsAsync(string userGoal);
}