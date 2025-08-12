using YogaApp.Domain.Entities;

namespace YogaApp.Application.RespositoryInterfaces;

public interface IPoseRepository
{
    Task<List<Pose>> GetAllPosesAsync();
    Task<Pose> GetPoseByIdAsync(int id);
    Task<Pose> UpdatePoseAsync(Pose pose);
    Task<int> CreatePoseAsync(Pose pose);
    Task SavePoseCategoryAsync(int poseId, List<int> categories);//assists in creating a new pose by adding to pose mapping
    Task DeletePoseByIdAsync(int id);
    Task<List<int>> GetPoseIdsByCategoryIdAsync(int catId);
    Task<List<int>> GetPoseIdsByDifficultyIdAsync(int difficultyId);
    
    Task<List<(int PoseId, string PoseName)>> GetPoseLinkByPoseId(List<int> poseId);
    

}