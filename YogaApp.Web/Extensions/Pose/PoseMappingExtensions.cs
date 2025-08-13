using YogaApp.Application.UseCases.GetAllPoses;
using YogaApp.Web.Models;

namespace YogaApp.Web.Extensions.Pose;

public static class PoseMappingExtensions
{
    public static PosesByDifficultyViewModel ToViewAllModel(this PosesByDifficultyDto dto)
    {
        return new PosesByDifficultyViewModel
        {
            EasyPoses = dto.EasyPoses.Select(p => new AllPosesViewModel(p)).ToList(),
            MediumPoses = dto.MediumPoses.Select(p => new AllPosesViewModel(p)).ToList(),
            HardPoses = dto.HardPoses.Select(p => new AllPosesViewModel(p)).ToList(),
        };
    }
}