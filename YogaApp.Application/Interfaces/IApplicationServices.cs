using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IApplicationServices
{
    Task<Pose> CreatePoseAsync(CreatePoseRequest request);
    Task<List<GetAllCategoriesResponse>> GetAllCategoriesAsync();
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    Task<List<GetAllPosesResponse>> GetAllPosesAsync();
    Task<GetPoseByIdResponse> GetPoseByIdAsync(int PoseId);
    Task<GetCatByCatIdResponse> GetCatByCatIdAsync(int CatId);
}