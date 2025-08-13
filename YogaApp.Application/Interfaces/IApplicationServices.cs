using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IApplicationServices
{
    Task<int> CreatePoseInDbAsync(CreatePoseRequest request);
    Task<List<GetAllCategoriesResponse>> GetAllCategoriesAsync();
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    Task<List<GetAllPosesResponse>> GetAllPosesAsync();
    Task<GetPoseByIdResponse> GetPoseByIdAsync(int PoseId);
    Task<GetCatByCatIdResponse> GetCatByCatIdAsync(int CatId);
    Task<GetDifficultyByIdResponse> GetDifficultyByIdAsync(int DifficultyId);
    Task<UpdatePoseRequest> UpdatePoseAsync(int poseId);
    
}