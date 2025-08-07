namespace YogaApp.Domain.Entities;

public class Difficulty
{
    public int DifficultyId { get; set; }
    public string DifficultyLevel { get; set; }
    List<int> PosesInThisDifficulty { get; set; }
}