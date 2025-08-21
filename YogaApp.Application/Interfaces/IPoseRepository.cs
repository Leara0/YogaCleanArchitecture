using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface IPoseRepository
{
    Task<List<Pose>> GetAllPosesAsync();
    Task<Pose> GetPoseByIdAsync(int id);
    Task UpdateToDbPoseAsync(Pose pose);
    Task<int> CreatePoseAsync(Pose pose);
    Task<List<int>> GetPoseIdsByCategoryIdAsync(int catId);
    Task<List<int>> GetPoseIdsByDifficultyIdAsync(int difficultyId);
    Task<List<Pose>> GetPosesByPoseIdsAsync(List<int> poseIds);
    Task DeletePoseByPoseIdAsync(int poseId);
    

}