using YogaApp.Application.DTO;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Application.UseCases.DeletePose;
using YogaApp.Application.UseCases.GetAllDifficulties;
using YogaApp.Application.UseCases.GetAllPoses;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using YogaApp.Application.UseCases.UpdatePose;
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
    private readonly IUpdatePoseUseCase _updatePoseUseCase;
    private readonly IDeletePoseByPoseIdUseCase _deletePoseByPoseIdUseCase;

    public ApplicationService(IGetAllCategoriesUseCase getAllCategories,
        IGetAllDifficultiesUseCase getAllDifficulties, ICreatePoseUseCase createPoseUseCase, IGetAllPosesUseCase getAllPosesUseCase,
        IGetPoseByIdUseCase getPoseByIdUseCase, IGetCatByCatIdUseCase getCatByCatIdUseCase, 
        IGetDifficultyByIdUseCase getDifficultyByIdUseCase, IUpdatePoseUseCase updatePoseUseCase,
        IDeletePoseByPoseIdUseCase deletePoseByPoseIdUseCase)
    {
        _getAllCategories = getAllCategories;
        _getAllDifficulties = getAllDifficulties;
        _createPoseUseCase = createPoseUseCase;
        _getAllPosesUseCase = getAllPosesUseCase;
        _getPoseByIdUseCase = getPoseByIdUseCase;
        _getCatByCatIdUseCase = getCatByCatIdUseCase;
        _getDifficultyByIdUseCase = getDifficultyByIdUseCase;
        _updatePoseUseCase = updatePoseUseCase;
        _deletePoseByPoseIdUseCase = deletePoseByPoseIdUseCase;
    }
    public async Task<int> CreatePoseInDbAsync(CreatePoseRequestDto requestDto)
    {
        var poseId = await _createPoseUseCase.ExecuteCreatePoseInDbAsync(requestDto);
        Console.WriteLine($"Application services returning Id: {poseId}");
        return poseId;
    }

    public async Task<List<GetAllCategoriesResponseDto>> GetAllCategoriesAsync()
    {
        return await _getAllCategories.ExecuteGetAllCategoriesAsync();
    }

    public async Task<List<GetAllDifficultiesResponseDto>> GetAllDifficultiesAsync()
    {
        return await _getAllDifficulties.ExecuteGetAllDifficultiesAsync();
    }

    public async Task<PosesByDifficultyDto> GetAllPosesAsync()
    {
        return await _getAllPosesUseCase.ExecuteGetAllPosesAsync();
    }

    public async Task<GetPoseByIdResponseDto> GetPoseByIdAsync(int PoseId)
    {
        return await _getPoseByIdUseCase.ExecuteGetPoseByIdAsync(PoseId);
    }

    public async Task<GetCatByCatIdResponseDto> GetCatByCatIdAsync(int CatId)
    {
        return await _getCatByCatIdUseCase.ExecuteGetCatByCatIdAsync(CatId);
    }

    public async Task<GetDifficultyByIdResponseDto> GetDifficultyByIdAsync(int DiffId)
    {
        return await _getDifficultyByIdUseCase.ExecuteGetDifficultyById(DiffId);
    }

    public async Task<UpdatePoseResponseDto> UpdatePoseAsync(int poseId)
    {
        return await _updatePoseUseCase.ExecuteUpdatePoseAsync(poseId);
    }

    public async Task UpdatePoseToDbAsync(UpdatePoseRequestToDbDto requestDto)
    {
        await _updatePoseUseCase.ExecuteUpdatePoseToDbAsync(requestDto);
    }

    public async Task DeletePoseAsync(int poseId)
    {
        await _deletePoseByPoseIdUseCase.ExecuteDeletePoseByPoseIdAsync(poseId);
    }
}

