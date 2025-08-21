using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;

namespace YogaApp.Application.Tests.UseCases.Difficulty;

public class GetDifficultyByDiffIdUseCaseTests
{
    [Fact]
    public async Task ExecuteGetDifficultyById_CallsRepositories_Once()
    {
        // ARRANGE
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        var mockPoseRepo = new Mock<IPoseRepository>();
    
        mockPoseRepo.Setup(p => p.GetPoseIdsByDifficultyIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockPoseRepo.Setup(p => p.GetPoseLinkByPoseIdAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<(int PoseId, string PoseName, string ThumbnailSvg)>());

        var useCase = new GetDifficultyByIdUseCase(mockDiffRepo.Object, mockPoseRepo.Object);

        // ACT
        await useCase.ExecuteGetDifficultyById(2);

        // ASSERT
        mockPoseRepo.Verify(p => p.GetPoseIdsByDifficultyIdAsync(2), Times.Once);
        mockPoseRepo.Verify(p => p.GetPoseLinkByPoseIdAsync(It.IsAny<List<int>>()), Times.Once);
    }

}