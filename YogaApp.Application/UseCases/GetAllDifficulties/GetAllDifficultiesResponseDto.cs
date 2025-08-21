using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCases.GetAllDifficulties;

public class GetAllDifficultiesResponseDto
{
    public int DifficultyId { get; set; }
    public string DifficultyLevel { get; set; }

    public GetAllDifficultiesResponseDto(Difficulty difficulty)
    {
        DifficultyId = difficulty.DifficultyId;
        DifficultyLevel = difficulty.DifficultyLevel;
    }
}