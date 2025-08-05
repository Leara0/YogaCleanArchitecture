namespace YogaApp.Infrastructure.DTO;

public class PoseDto
{
    public int Pose_Id { get; set; }
    public string English_Name { get; set; }
    public string Sanskrit_Name_Adapted { get; set; }
    public string Sanskrit_Name { get; set; }
    public string Translation_Name { get; set; }
    public string Pose_Description { get; set; }
    public string Pose_Benefits { get; set; }
    public int Difficulty_Id { get; set; }
    public string Url_Svg { get; set; }
    public string Url_Svg_Alt { get; set; }
    
    public string Difficulty_Level { get; set; }


}