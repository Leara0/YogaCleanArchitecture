using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.UpdatePose;

namespace YogaApp.Application.Tests.UseCases.Pose;

public class UpdatePoseUseCaseTests
{
    [Fact]
    public async Task ExecuteUpdatePoseAsync_CallsAllRepositories_Once()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();
        
        //tell the repo how to respond - minimal data just enough to check it was called
        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Pose("Test", 1));
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockDiffRepo.Setup(d => d.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<Domain.Entities.Difficulty>());
        mockCatRepo.Setup(c => c.GetAllCategoriesAsync())
            .ReturnsAsync(new List<Domain.Entities.Category>());
        
        var useCase = new UpdatePoseUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
        
        //ACT
        await useCase.ExecuteUpdatePoseAsync(5);
        
        //ASSERT
        //test that repositories were all called exactly one time
        mockPoseRepo.Verify(p => p.GetPoseByIdAsync(5), Times.Once);
        mockCatRepo.Verify(c => c.GetCategoryIdsByPoseIdAsync(5), Times.Once);
        mockDiffRepo.Verify(d => d.GetAllDifficultiesAsync(), Times.Once);
        mockCatRepo.Verify(c => c.GetAllCategoriesAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteUpdatePoseAsync_MapsDifficultiesToSelectOptions()
    {
        //ARRANGE - focus on difficulty mapping
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        var difficulties = new List<Domain.Entities.Difficulty>
        {
            new Domain.Entities.Difficulty { DifficultyId = 1, DifficultyLevel = "Beginner" },
            new Domain.Entities.Difficulty { DifficultyId = 2, DifficultyLevel = "Intermediate" },
        };
        //minimal setup for other repos
        mockPoseRepo.Setup(p=> p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Pose("Test", 1));
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockDiffRepo.Setup(d => d.GetAllDifficultiesAsync())
            .ReturnsAsync(difficulties);
        mockCatRepo.Setup(c => c.GetAllCategoriesAsync())
            .ReturnsAsync(new List<Domain.Entities.Category>());
        
        var useCase = new UpdatePoseUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteUpdatePoseAsync(5);
        
        //ASSERT - testing difficulty mapping
        Assert.Equal(2, result.DifficultyOptions.Count());
        Assert.Contains(result.DifficultyOptions, d => d.Value == "1" && d.Text == "Beginner");
        Assert.Contains(result.DifficultyOptions, d => d.Value == "2" && d.Text == "Intermediate");
    }

    [Fact]
    public async Task ExecuteUpdatePoseAsync_MapsCategoriesToSelectOptions()
    {
        //ARRANGE - focus on category mapping
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockDiffRepo = new Mock<IDifficultyRepository>();

        var categories = new List<Domain.Entities.Category>
        {
            new Domain.Entities.Category { CategoryId = 1, CategoryName = "Standing" },
            new Domain.Entities.Category { CategoryId = 2, CategoryName = "Seated" }
        };
        
        //minimal setup for other repos
        mockPoseRepo.Setup(p => p.GetPoseByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Domain.Entities.Pose("Test", 1));
        mockCatRepo.Setup(c => c.GetCategoryIdsByPoseIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<int>());
        mockDiffRepo.Setup(d => d.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<Domain.Entities.Difficulty>());
        mockCatRepo.Setup(c => c.GetAllCategoriesAsync())
            .ReturnsAsync(categories);
        
        var useCase = new UpdatePoseUseCase(mockCatRepo.Object, mockPoseRepo.Object, mockDiffRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteUpdatePoseAsync(3);
        
        //ASSERT - testing category mapping
        Assert.Equal(2, result.CategoryOptions.Count());
        Assert.Contains(result.CategoryOptions, c=> c.Value == "1" && c.Text == "Standing");
        Assert.Contains(result.CategoryOptions, c => c.Value == "2" && c.Text == "Seated");
    }

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