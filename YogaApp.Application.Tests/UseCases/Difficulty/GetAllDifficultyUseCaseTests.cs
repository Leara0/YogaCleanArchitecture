using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Difficulty;

public class GetAllDifficultyUseCaseTests
{
   [Fact]
    public async Task ExecuteGetAllDifficultiesAsync_CallsRepository_Once()
    {
        //ARRANGE
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        mockDiffRepo.Setup(d=> d.GetAllDifficultiesAsync()).
            ReturnsAsync(new List<Domain.Entities.Difficulty>());
        
        var useCase = new GetAllDifficultiesUseCase(mockDiffRepo.Object);
        
        //ACT
        await useCase.ExecuteGetAllDifficultiesAsync();
        
        //ASSERT
        mockDiffRepo.Verify(d => d.GetAllDifficultiesAsync(), Times.Once);
    }
}