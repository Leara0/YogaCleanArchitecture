using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllPoses;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.UseCaseInterfaces;

public interface IApplicationServices
{
    Task<int> CreatePoseInDbAsync(CreatePoseRequestDto requestDto);
    Task<List<GetAllCategoriesResponseDto>> GetAllCategoriesAsync();
    Task<List<Difficulty>> GetAllDifficultiesAsync();
    Task<PosesByDifficultyDto> GetAllPosesAsync();
    Task<GetPoseByIdResponseDto> GetPoseByIdAsync(int PoseId);
    Task<GetCatByCatIdResponseDto> GetCatByCatIdAsync(int CatId);
    Task<GetDifficultyByIdResponseDto> GetDifficultyByIdAsync(int DifficultyId);
    Task<UpdatePoseResponseDto> UpdatePoseAsync(int poseId);
    
}