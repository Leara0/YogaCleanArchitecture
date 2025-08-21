using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Pose;

public class GetPoseByPoseIdUseCaseTests

{
    [Fact]
    public async Task ExecuteGetPoseByIdAsync_CallsAllRepositories_Once()
    {
        // ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Pose("Test", 1));
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockCatRepo.Setup(c => c.GetCatsInPoseAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<(int CatId, string CatName)>());

        var useCase = new GetPoseByIdUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);

        // ACT
        await useCase.ExecuteGetPoseByIdAsync(5);

        // ASSERT
        mockPoseRepo.Verify(p => p.GetPoseByIdAsync(5), Times.Once);
        mockCatRepo.Verify(c => c.GetCategoryIdsByPoseIdAsync(5), Times.Once);
        mockCatRepo.Verify(c => c.GetCatsInPoseAsync(It.IsAny<List<int>>()), Times.Once);
    }

    
    [Theory]
    [InlineData(1, "Beginner")]
    [InlineData(2, "Intermediate")]
    [InlineData(3, "Advanced")]
    public async Task ExecuteGetPoseByIdAsync_MapsDifficultyCorrectly(int difficultyId, string expectedText)
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        
        var pose = new Domain.Entities.Pose("Test Pose", difficultyId);
        
        //tell the mock repos how to respond
        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>())).ReturnsAsync(pose);
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockCatRepo.Setup(c => c.GetCatsInPoseAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<(int, string)>());

        var useCase = new GetPoseByIdUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object); 
        
        //ACT
        var result = await useCase.ExecuteGetPoseByIdAsync(1);
        
        //ASSERT
        Assert.Equal(expectedText, result.DifficultyLink.DifficultyName);
    }

    [Fact]
    public async Task ExecuteGetPoseByIdAsync_MapsCategoriesToLinks()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        var pose = new Domain.Entities.Pose("Test Pose", 1);
        var categoryIds = new List<int> { 2, 5, 8 };
        var categoryTuples = new List<(int CatId, string CatName)>
        {
            (2, "Core Yoga"),
            (5, "Backbend Yoga"),
            (8, "Standing Yoga")
        };

        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pose);
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(categoryIds);
        mockCatRepo.Setup(c => c.GetCatsInPoseAsync(categoryIds))
            .ReturnsAsync(categoryTuples);
        
        var useCase = new GetPoseByIdUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteGetPoseByIdAsync(8);
        
        //ASSERT
        Assert.Equal(3, result.CategoryLink.Count());
        
        //verify specific mapping from tuple to CategoryLinkDto
        var coreYogaLink = result.CategoryLink.FirstOrDefault(c => c.CategoryId == 2);
        Assert.NotNull(coreYogaLink);
        Assert.Equal("Core Yoga", coreYogaLink.CategoryName);

        var backbendingLink = result.CategoryLink.FirstOrDefault(c => c.CategoryId == 5);
        Assert.NotNull(backbendingLink);
        Assert.Equal("Backbend Yoga", backbendingLink.CategoryName);
        
        var standingYogaLink = result.CategoryLink.FirstOrDefault(c => c.CategoryId == 8);
        Assert.NotNull(standingYogaLink);
        Assert.Equal("Standing Yoga", standingYogaLink.CategoryName);
    }
    
    [Fact]
    public async Task ExecuteGetPoseByIdAsync_HandlesNoCategoriesGracefully()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        var pose = new Domain.Entities.Pose("Test Pose", 1);
    
        //tell mock repos to return empty category data
        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(pose);
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>()); // Empty list
        mockCatRepo.Setup(c => c.GetCatsInPoseAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<(int CatId, string CatName)>()); // Empty list

        var useCase = new GetPoseByIdUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
    
        //ACT
        var result = await useCase.ExecuteGetPoseByIdAsync(1);
    
        //ASSERT
        Assert.NotNull(result);
        Assert.NotNull(result.CategoryLink);
        Assert.Empty(result.CategoryLink);
    }
}