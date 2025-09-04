namespace YogaApp.Application.Interfaces;

public interface IAIYogaService
{
    Task<List<string>> GetPoseSuggestionsAsync(string userGoal);
}