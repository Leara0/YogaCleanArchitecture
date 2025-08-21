using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.UpdatePose;

namespace YogaApp.Application.Tests.UseCases.Pose;

public class UpdatePoseUseCaseTests
{
   

    [Fact]
    public async Task ExecuteUpdatePoseToDbAsync_CallsAllRepositories_WhenCategoriesExist()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        
        //basic setup for function
        mockPoseRepo.Setup(p => p.UpdateToDbPoseAsync(It.IsAny<Domain.Entities.Pose>()))
            .Returns(Task.CompletedTask);
        mockCatRepo.Setup(c => c.DeleteCategoriesByPoseIdAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        mockCatRepo.Setup(c => c.AddCategoryByPoseIdAsync(It.IsAny<int>(), It.IsAny<List<int>>()))
            .Returns(Task.CompletedTask);
        
        var useCase = new UpdatePoseUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);

        var dto = new UpdatePoseRequestToDbDto()
        {
            PoseId = 5,
            PoseName = "Test Pose",
            DifficultyId = 3,
            CategoryIds = new List<int> { 1, 2, 3 }
        };
        
        //ACT
        await useCase.ExecuteUpdatePoseToDbAsync(dto);
        
        //ASSERT - check they were all called
        mockPoseRepo.Verify(p => p.UpdateToDbPoseAsync(It.IsAny<Domain.Entities.Pose>()), Times.Once);
        mockCatRepo.Verify(c => c.DeleteCategoriesByPoseIdAsync(5), Times.Once);
        mockCatRepo.Verify(c => c.AddCategoryByPoseIdAsync(5, dto.CategoryIds), Times.Once);
    }

    [Fact]
    public async Task ExecuteUpdatePoseToDbAsync_DoesNotCallAddCategories_WhenNoCategoriesExist()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        
        mockPoseRepo.Setup(p => p.UpdateToDbPoseAsync(It.IsAny<Domain.Entities.Pose>()))
            .Returns(Task.CompletedTask);
        mockCatRepo.Setup(c => c.DeleteCategoriesByPoseIdAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        
        var useCase = new UpdatePoseUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);

        var dto = new UpdatePoseRequestToDbDto
        {
            PoseId = 5,
            PoseName = "Test Pose",
            DifficultyId = 3,
            CategoryIds = null // no categories
        };
        
        //ACT
        await useCase.ExecuteUpdatePoseToDbAsync(dto);
        
        //ASSERT
        mockPoseRepo.Verify(p => p.UpdateToDbPoseAsync(It.IsAny<Domain.Entities.Pose>()), Times.Once);
        mockCatRepo.Verify(c => c.DeleteCategoriesByPoseIdAsync(5), Times.Once);
        mockCatRepo.Verify(c => 
            c.AddCategoryByPoseIdAsync(It.IsAny<int>(), It.IsAny<List<int>>()), Times.Never);
        
    }
    
}