using Moq;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;

namespace YogaApp.Application.Tests.UseCases.Difficulty;

public class GetDifficultyByDiffIdUseCaseTests
{
    [Fact]
    public async Task ExecuteGetDifficultyById_CallsRepositories_Once()
    {
        // ARRANGE
        var mockSearchServices = new Mock<IPoseSearchService>();
    
        mockSearchServices.Setup(p => p.GetPoseIdsByDifficultyIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockSearchServices.Setup(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<Domain.Entities.Pose>());

        var useCase = new GetDifficultyByIdUseCase(mockSearchServices.Object);

        // ACT
        await useCase.ExecuteGetDifficultyById(2);

        // ASSERT
        mockSearchServices.Verify(p => p.GetPoseIdsByDifficultyIdAsync(2), Times.Once);
        mockSearchServices.Verify(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()), Times.Once);
    }

    [Theory]
    [InlineData(1, "Beginner")]
    [InlineData(2, "Intermediate")]
    [InlineData(3, "Advanced")]
    public async Task ExecuteGetDifficultyById_MapsDifficultyCorrectly(int diffId, string expectedName)
    {
        //ARRANGE
        var mockSearchServices = new Mock<IPoseSearchService>();

        mockSearchServices.Setup(p => p.GetPoseIdsByDifficultyIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockSearchServices.Setup(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<Domain.Entities.Pose>());
        
        var useCase = new GetDifficultyByIdUseCase(mockSearchServices.Object);
        
        //ACT
        var result = await useCase.ExecuteGetDifficultyById(diffId);
        
        //ASSERT
        Assert.Equal(expectedName, result.DifficultyLinkDto.DifficultyName);
    }

    [Fact]
    public async Task ExecuteGetDifficultyById_MapsPosesToPoseLinksCorrectly()
    {
        var mockSearchServices = new Mock<IPoseSearchService>();

        var poses = new List<Domain.Entities.Pose>
        {
            new Domain.Entities.Pose("Warrior I", 2) { PoseId = 1, ThumbnailUrlSvg = "url1.svg" },
            new Domain.Entities.Pose("Warrior II", 2) { PoseId = 2, ThumbnailUrlSvg = "url2.svg" }
        };

        mockSearchServices.Setup(p => p.GetPoseIdsByDifficultyIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int> { 1, 2 });
        mockSearchServices.Setup(p => p.GetPosesByPoseIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(poses);
        
        var useCase = new GetDifficultyByIdUseCase(mockSearchServices.Object);
        
        //ACT
        var result = await useCase.ExecuteGetDifficultyById(2);
        
        //ASSERT
        Assert.Equal(2, result.PoseLinks.Count);
        Assert.Contains(result.PoseLinks, p=> p.PoseId == 1 && p.PoseName == "Warrior I");
        Assert.Contains(result.PoseLinks, p => p.PoseId == 2 && p.PoseName == "Warrior II");
    }

}