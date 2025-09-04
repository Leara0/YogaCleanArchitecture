using YogaApp.Domain.Entities;

namespace YogaApp.Application.Interfaces;

public interface IPoseSearchService
{
    Task<List<Pose>> SearchByNameAsync(string name);
    Task<List<Pose>> SearchByBenefitsAsync(string benefits);
    Task<List<Pose>> SearchByDescriptionAsync(string description);
    Task<List<int>> GetPoseIdsByCategoryIdAsync(int categoryId);
    Task<List<int>> GetPoseIdsByDifficultyIdAsync(int difficultyId);
    Task<List<Pose>> GetPosesByPoseIdsAsync(List<int> poseIds);
    Task<Pose?> SearchForSingleNameAsync(string name);
}