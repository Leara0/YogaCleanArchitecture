using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Pose;

public class GetPoseByPoseIdUseCaseTests
{
    [Fact]
    public async Task ExecuteGetPoseByIdAsync_ReturnsCompleteDto_WhenAllDataExists()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        
        //create mock pose data to for repository
        var pose = new Domain.Entities.Pose("Downward Dog", 2);
        var categoryIds = new List<int> { 1, 3 };
        var categoryTuples = new List<(int CatId, string CatName)>
        {
            (1, "Standing"),
            (3, "Strengthening")
        };
        
        //tell mock repos what to do when called (return mock pose data)
        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(3)).ReturnsAsync(pose);
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(3)).ReturnsAsync(categoryIds);
        mockCatRepo.Setup(c => c.GetCatsInPoseAsync(categoryIds)).ReturnsAsync(categoryTuples);

        var useCase = new GetPoseByIdUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
        
        //ACT - call the use case
        var result = await useCase.ExecuteGetPoseByIdAsync(3);
        
        //ASSERT
        Assert.NotNull(result);
        //Tests switch statement
        Assert.Equal("Intermediate", result.DifficultyLink.DifficultyName);
        //Tests tuple mapping
        Assert.Equal(2, result.CategoryLink.Count());
        Assert.Contains(result.CategoryLink, c => c.CategoryName == "Standing");
        Assert.Contains(result.CategoryLink, c => c.CategoryName == "Strengthening");
    }
}