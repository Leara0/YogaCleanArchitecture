using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases;

namespace YogaApp.Application.Tests.UseCases.Category;

public class GetAllCategoriesUseCaseTests
{
    [Fact]
    public async Task ExecuteGetAllCategories_CallsRepository_Once()
    {
        //ARRANGE
        var mockCatRepo = new Mock<ICategoryRepository>();
        
        mockCatRepo.Setup(c => c.GetAllCategoriesAsync())
            .ReturnsAsync(new List<Domain.Entities.Category>());

        var useCase = new GetAllCategoriesUseCase(mockCatRepo.Object);
        
        //ACT
        await useCase.ExecuteGetAllCategoriesAsync();
        
        //ASSERT
        mockCatRepo.Verify(c => c.GetAllCategoriesAsync(), Times.Once);
    }
}