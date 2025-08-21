using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Category;

public class GetCategoryByCatIdUseCaseTests
{
    [Fact]
    public async Task ExecuteGetCatByCatIdAsync_ShouldCallRepositories_Once()
    {
        //ARRANGE
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockPoseRepo = new Mock<IPoseRepository>();

        mockCatRepo.Setup(c => c.GetCategoryByCatIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Category());
        mockPoseRepo.Setup(p => p.GetPoseIdsByCategoryIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockPoseRepo.Setup(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<Domain.Entities.Pose>());

        var useCase = new GetCatByCatIdUseCase(mockCatRepo.Object, mockPoseRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteGetCatByCatIdAsync(5);
        
        //ASSERT
        mockCatRepo.Verify(c => c.GetCategoryByCatIdAsync(5), Times.Once);
        mockPoseRepo.Verify(p=> p.GetPoseIdsByCategoryIdAsync(5), Times.Once);
        mockPoseRepo.Verify(p=> p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteGetCatByCatIdAsync_MapsPosesToPoseLinks()
    {
        //ARRANGE
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockPoseRepo = new Mock<IPoseRepository>();

        var poses = new List<Domain.Entities.Pose>
        {
            new Domain.Entities.Pose("Tree Pose", 1) { PoseId = 1, ThumbnailUrlSvg = "url1.svg" },
            new Domain.Entities.Pose("Mountain Pose", 1) { PoseId = 2, ThumbnailUrlSvg = "url2.svg" },
        };

        mockCatRepo.Setup(c => c.GetCategoryByCatIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Category());
        mockPoseRepo.Setup(p => p.GetPoseIdsByCategoryIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int> { 1, 2 });
        mockPoseRepo.Setup(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(poses);
        
        var useCase = new GetCatByCatIdUseCase(mockCatRepo.Object, mockPoseRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteGetCatByCatIdAsync(3);
        
        //ASSERT
        Assert.Equal(2, result.PoseLink.Count);
        Assert.Contains(result.PoseLink, p=> p.PoseId == 1 && p.PoseName == "Tree Pose");
        Assert.Contains(result.PoseLink, p=> p.PoseId == 2 && p.PoseName == "Mountain Pose");
    }
}