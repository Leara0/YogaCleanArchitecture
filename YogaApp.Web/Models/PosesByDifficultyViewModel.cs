namespace YogaApp.Web.Models;

public class PosesByDifficultyViewModel
{
    public List<AllPosesViewModel> EasyPoses { get; set; } = new List<AllPosesViewModel>();
    public List<AllPosesViewModel> MediumPoses { get; set; } = new List<AllPosesViewModel>();
    public List<AllPosesViewModel> HardPoses { get; set; } = new List<AllPosesViewModel>();
}