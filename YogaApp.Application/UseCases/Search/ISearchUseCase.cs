namespace YogaApp.Application.UseCases.Search;

public interface ISearchUseCase
{
    Task<SearchResponseDto> ExecuteSearchAsync(string searchString);
}