using YogaApp.Application.DTO;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.Services;

public class ApplicationService :IApplicationServices
{
    private readonly IGetAllCategoriesUseCase _getAllCategories;
    private readonly IGetAllDifficultiesUseCase _getAllDifficulties;
    private readonly ICreatePoseInDbUseCase _createPoseInDbUseCase;
    private readonly  IGetAllPosesUseCase _getAllPosesUseCase;
    private readonly IGetPoseByIdUseCase _getPoseByIdUseCase;
    private readonly IGetCatByCatIdUseCase _getCatByCatIdUseCase;
    private readonly IGetDifficultyByIdUseCase _getDifficultyByIdUseCase;
    private readonly IUpdatePoseUseCase _updatePoseUseCase;

    public ApplicationService(IGetAllCategoriesUseCase getAllCategories,
        IGetAllDifficultiesUseCase getAllDifficulties, ICreatePoseInDbUseCase createPoseInDbUseCase, IGetAllPosesUseCase getAllPosesUseCase,
        IGetPoseByIdUseCase getPoseByIdUseCase, IGetCatByCatIdUseCase getCatByCatIdUseCase, 
        IGetDifficultyByIdUseCase getDifficultyByIdUseCase, IUpdatePoseUseCase updatePoseUseCase)
    {
        _getAllCategories = getAllCategories;
        _getAllDifficulties = getAllDifficulties;
        _createPoseInDbUseCase = createPoseInDbUseCase;
        _getAllPosesUseCase = getAllPosesUseCase;
        _getPoseByIdUseCase = getPoseByIdUseCase;
        _getCatByCatIdUseCase = getCatByCatIdUseCase;
        _getDifficultyByIdUseCase = getDifficultyByIdUseCase;
        _updatePoseUseCase = updatePoseUseCase;
    }
    public async Task<int> CreatePoseInDbAsync(CreatePoseRequest request)
    {
        return await _createPoseInDbUseCase.ExecuteCreatePoseAsync(request);
    }

    public async Task<List<GetAllCategoriesResponse>> GetAllCategoriesAsync()
    {
        return await _getAllCategories.ExecuteGetAllCategoriesAsync();
    }

    public async Task<List<Difficulty>> GetAllDifficultiesAsync()
    {
        return await _getAllDifficulties.ExecuteGetAllDifficultiesAsync();
    }

    public async Task<List<GetAllPosesResponse>> GetAllPosesAsync()
    {
        return await _getAllPosesUseCase.ExecuteGetAllPosesAsync();
    }

    public async Task<GetPoseByIdResponse> GetPoseByIdAsync(int PoseId)
    {
        return await _getPoseByIdUseCase.ExecuteGetPoseByIdAsync(PoseId);
    }

    public async Task<GetCatByCatIdResponse> GetCatByCatIdAsync(int CatId)
    {
        return await _getCatByCatIdUseCase.ExecuteGetCatByCatIdAsync(CatId);
    }

    public async Task<GetDifficultyByIdResponse> GetDifficultyByIdAsync(int DiffId)
    {
        return await _getDifficultyByIdUseCase.ExecuteGetDifficultyById(DiffId);
    }

    public async Task<UpdatePoseRequest> UpdatePoseAsync(int poseId)
    {
        return await _updatePoseUseCase.ExecuteUpdatePoseAsync(poseId);
    }
}

