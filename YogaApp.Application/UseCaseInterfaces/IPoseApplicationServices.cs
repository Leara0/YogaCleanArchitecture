using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IPoseApplicationServices
{
    Task<Pose> CreatePoseAsync(CreatePoseRequest request);
    Task<List<GetAllCategoriesResponse>> GetAllCategoriesAsync();
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    Task<List<GetAllPosesResponse>> GetAllPosesAsync();
    Task<GetPoseByIdResponse> GetPoseByIdAsync(int PoseId);
}