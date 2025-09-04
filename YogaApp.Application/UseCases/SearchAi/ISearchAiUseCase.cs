using YogaApp.Application.DTO;

namespace YogaApp.Application.UseCases.SearchAi;

public interface ISearchAiUseCase
{
    Task<SearchAiResponseDto> ExecuteGetAiSuggestionsAsync(string userGoal);
}