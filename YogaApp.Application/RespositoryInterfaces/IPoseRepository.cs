using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface IPoseRepository
{
    Task<List<Pose>> GetAllPosesAsync();
    Task<Pose> GetPoseByIdAsync(int id);
    Task<Pose> UpdatePoseAsync(Pose pose);
    Task<int> CreatePoseAsync(Pose pose);
    Task SavePoseCategoryAsync(int poseId, List<int> categories);
    Task DeletePoseByIdAsync(int id);
    

}