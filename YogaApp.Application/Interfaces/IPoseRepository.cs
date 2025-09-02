using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface IPoseRepository
{
    Task<List<Pose>> GetAllPosesAsync();
    Task<Pose> GetPoseByIdAsync(int id);
    Task UpdateToDbPoseAsync(Pose pose);
    Task<int> CreatePoseAsync(Pose pose);
    Task DeletePoseByPoseIdAsync(int poseId);
    

}