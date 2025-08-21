using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;
using YogaApp.Domain.Entities;
using PoseEntity = YogaApp.Domain.Entities.Pose;  // Create alias

namespace YogaApp.Application.Tests.UseCases.Pose;

public class GetAllPosesUseCaseTests
{
    [Fact]
    public async Task ExecuteGetAllPosesAsync_CallsRepository_Once()
    {
        // ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        
        mockPoseRepo.Setup(p => p.GetAllPosesAsync())
            .ReturnsAsync(new List<PoseEntity>());

        var useCase = new GetAllPosesUseCase(mockPoseRepo.Object);

        // ACT
        await useCase.ExecuteGetAllPosesAsync();

        // ASSERT
        mockPoseRepo.Verify(p => p.GetAllPosesAsync(), Times.Once);
    }

    
    [Fact]
    public async Task ExecuteGetAllPoses_GroupsPosesByDifficulty()
    {
        //ARRANGE
        //set up mock repo and sample pose data for repo return
        var mockPoseRepo = new Mock<IPoseRepository>();
        var samplePoses = new List<PoseEntity>
        {
            new PoseEntity("Easy Pose 1", 1),
            new PoseEntity("Easy Pose 2", 1),
            new PoseEntity("Medium Pose 1", 2),
            new PoseEntity("Difficult Pose 1", 3),
            new PoseEntity("Difficult Pose 2", 3),
            new PoseEntity("Difficult Pose 3", 3),
        };
        //tell mock repo what to do when called
        mockPoseRepo.Setup(p => p.GetAllPosesAsync()).ReturnsAsync(samplePoses);
        
        var useCase = new GetAllPosesUseCase(mockPoseRepo.Object);
        
        //ACT - call the use case
        var result = await useCase.ExecuteGetAllPosesAsync();
        
        //ASSERT - check that it returns the right number of poses in each difficulty level
        Assert.Equal(2, result.EasyPoses.Count);
        Assert.Single(result.MediumPoses);
        Assert.Equal(3, result.HardPoses.Count);
    }
}