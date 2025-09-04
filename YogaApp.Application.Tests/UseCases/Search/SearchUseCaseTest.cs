using Moq;
using YogaApp.Application.DTO;
using YogaApp.Application.Interfaces;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.Search;
using YogaApp.Application.UseCases.UpdatePose;
using PoseEntity = YogaApp.Domain.Entities.Pose;
using CatEntity = YogaApp.Domain.Entities.Category;

namespace YogaApp.Application.Tests.UseCases.Search;

public class SearchUseCaseTest
{
    [Fact]
    public async Task ExecuteSearchAsync_CallsAllRepositories_Once()
    {
        //ARRANGE
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockSearchService = new Mock<IPoseSearchServices>();
       
        //basic setup for function
        mockCatRepo.Setup(c => c.SearchByCategoryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<(int CategoryId, string Name)>());
        mockSearchService.Setup(s=> s.SearchByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<PoseEntity>());
        mockSearchService.Setup(s => s.SearchByDescriptionAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<PoseEntity>());
        mockSearchService.Setup(s => s.SearchByBenefitsAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<PoseEntity>());
        
        var useCase = new SearchUseCase(mockSearchService.Object, mockCatRepo.Object);
        var searchString = "test";

        //ACT
        await useCase.ExecuteSearchAsync(searchString);
        
        //ASSERT - check they were all called
        mockCatRepo.Verify(c => c.SearchByCategoryAsync(searchString), Times.Once);
        mockSearchService.Verify(s=> s.SearchByNameAsync(searchString), Times.Once);
        mockSearchService.Verify(s => s.SearchByDescriptionAsync(searchString), Times.Once);
        mockSearchService.Verify(s => s.SearchByBenefitsAsync(searchString), Times.Once);
    }

    [Fact]
    public async Task ExecuteSearchAsync_ReturnsAllSearchResults_InCorrectStructure()
    {
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockSearchService = new Mock<IPoseSearchServices>();
        
        //create mock poses for each search type
        var namePoses = new List<PoseEntity>
        {
            new PoseEntity("Warrior 1", 1),
            new PoseEntity("Warrior 2", 1)
        };
        var descPoses = new List<PoseEntity>
        {
            new PoseEntity("Hand Stand", 3),
        };
        var benePoses = new List<PoseEntity>
        {
            new PoseEntity("Warrior 3", 1)
        };
        var categories = new List<(int CatId, string Name)>
        {
            new (1, "Standing"),
            new(2, "Arm Balances")
        };
        
        //tell mocks to return mock data
        mockSearchService.Setup(s => s.SearchByNameAsync("yoga")).ReturnsAsync(namePoses);
        mockSearchService.Setup(s => s.SearchByDescriptionAsync("yoga")).ReturnsAsync(descPoses);
        mockSearchService.Setup(s => s.SearchByBenefitsAsync("yoga")).ReturnsAsync(benePoses);
        mockCatRepo.Setup(c => c.SearchByCategoryAsync("yoga")).ReturnsAsync(categories);

        var useCase = new SearchUseCase(mockSearchService.Object, mockCatRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteSearchAsync("yoga");
        
        //ASSERT
        Assert.Equal(2, result.NameLinks.Count);
        Assert.Single(result.DescriptionLinks);
        Assert.Single(result.BenefitsLinks);
        Assert.Equal(2, result.CategoriesLinks.Count);
        
        //Verify specific mapping
        Assert.Equal("Warrior 1", result.NameLinks.First().PoseName);
        Assert.Equal("Standing", result.CategoriesLinks.First().CategoryName);
    }

    [Fact]
    public async Task ExecuteSearchAsync_HandlesNoResultsGracefully()
    {
        //ARRANGE
        var mockCatRepo = new Mock<ICategoryRepository>();
        var mockSearchService = new Mock<IPoseSearchServices>();

        mockSearchService.Setup(s => s.SearchByNameAsync("not found"))
            .ReturnsAsync(new List<PoseEntity>());
        mockSearchService.Setup(s => s.SearchByDescriptionAsync("not found"))
            .ReturnsAsync(new List<PoseEntity>());
        mockSearchService.Setup(s => s.SearchByBenefitsAsync("not found"))
            .ReturnsAsync(new List<PoseEntity>());
        mockCatRepo.Setup(c => c.SearchByCategoryAsync("not found"))
            .ReturnsAsync(new List<(int CatId, string Name)>());

        var useCase = new SearchUseCase(mockSearchService.Object, mockCatRepo.Object);
        
        //ACT
        var result = await useCase.ExecuteSearchAsync("not found");
        
        //ASSERT
        Assert.Empty(result.NameLinks);
        Assert.Empty(result.DescriptionLinks);
        Assert.Empty(result.BenefitsLinks);
        Assert.Empty(result.CategoriesLinks);
    }
}