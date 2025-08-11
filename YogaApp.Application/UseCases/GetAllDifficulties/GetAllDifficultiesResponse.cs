using YogaApp.Domain.Entities;

namespace YogaApp.Application.DTO;

public class GetAllDifficultiesResponse
{
    public int Difficulty_Id { get; set; }
    public string Difficulty_Level { get; set; }
    public List<Pose> EasyPoses { get; set; }
}