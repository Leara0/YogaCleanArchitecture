using YogaApp.Application.DTO;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Domain.Entities;

namespace YogaApp.Application.Services;

public class ApplicationService :IApplicationServices
{
    private readonly IGetAllCategoriesUseCase _getAllCategories;
    private readonly IGetAllDifficultiesUseCase _getAllDifficulties;
    private readonly ICreatePoseUseCase _createPoseUseCase;
    private readonly  IGetAllPosesUseCase _getAllPosesUseCase;
    private readonly IGetPoseByIdUseCase _getPoseByIdUseCase;
    private readonly IGetCatByCatIdUseCase _getCatByCatIdUseCase;
    private readonly IGetDifficultyByIdUseCase _getDifficultyByIdUseCase;

    public ApplicationService(IGetAllCategoriesUseCase getAllCategories,
        IGetAllDifficultiesUseCase getAllDifficulties, ICreatePoseUseCase createPoseUseCase, IGetAllPosesUseCase getAllPosesUseCase,
        IGetPoseByIdUseCase getPoseByIdUseCase, IGetCatByCatIdUseCase getCatByCatIdUseCase, 
        IGetDifficultyByIdUseCase getDifficultyByIdUseCase)
    {
        _getAllCategories = getAllCategories;
        _getAllDifficulties = getAllDifficulties;
        _createPoseUseCase = createPoseUseCase;
        _getAllPosesUseCase = getAllPosesUseCase;
        _getPoseByIdUseCase = getPoseByIdUseCase;
        _getCatByCatIdUseCase = getCatByCatIdUseCase;
        _getDifficultyByIdUseCase = getDifficultyByIdUseCase;
    }
    public async Task<Pose> CreatePoseAsync(CreatePoseRequest request)
    {
        return await _createPoseUseCase.ExecuteCreatePoseAsync(request);
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
}

