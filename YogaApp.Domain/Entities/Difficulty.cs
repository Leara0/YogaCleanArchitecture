namespace YogaApp.Domain.Entities;

public class Difficulty
{
    public int Difficulty_Id { get; set; }
    public string Difficulty_Level { get; set; }
    List<int> PosesInThisDifficulty { get; set; }
}