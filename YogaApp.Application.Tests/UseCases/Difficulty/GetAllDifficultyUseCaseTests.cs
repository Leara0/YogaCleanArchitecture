using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Difficulty;

public class GetAllDifficultyUseCaseTests
{
   /* [Fact]
    public async Task ExecuteGetAllDifficultiesAsync_CallsRepository_ReturnsAllDifficulties()
    {
        //ARRANGE
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        var expectedDifficulties = new List<Domain.Entities.Difficulty>
        {
            new Domain.Entities.Difficulty { DifficultyId = 1, DifficultyLevel = "Beginner" },
            new Domain.Entities.Difficulty { DifficultyId = 2, DifficultyLevel = "Intermediate" },
        };
        
        mockDiffRepo.Setup(d => d.GetAllDifficultiesAsync())
            .ReturnsAsync(expectedDifficulties);
        
        var useCase = new GetAllDifficultiesUseCase(mockDiffRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteGetAllDifficultiesAsync();
        
        //ASSERT
        mockDiffRepo.Verify(d => d.GetAllDifficultiesAsync(), Times.Once);
        Assert.Equal(expectedDifficulties, result);
    }*/
}