using Moq;
using YogaApp.Application.Interfaces;
using YogaApp.Application.UseCases.SearchAi;


namespace YogaApp.Application.Tests.UseCases.Search;

public class SearchAiUseCaseTests
{
    [Fact]
    public async Task ExecuteGetAiSuggestionsAsync_CallsAllServices_Correctly()
    {
        //ARRANGE
        var mockAiService = new Mock<IAIYogaService>();
        var mockSearchService = new Mock<IPoseSearchService>();

        var suggestedPoses = new List<string> { "Chair", "Tree" };
        
        mockAiService.Setup(ai => ai.GetPoseSuggestionsAsync(It.IsAny<string>()))
            .ReturnsAsync(suggestedPoses);
        mockSearchService.Setup(s => s.SearchForSingleNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new Domain.Entities.Pose("Any", 1));
        
        var useCase = new SearchAiUseCase(mockAiService.Object, mockSearchService.Object);
        
        //ACT
        await useCase.ExecuteGetAiSuggestionsAsync("goal");
        
        //ASSERT
        mockAiService.Verify(ai => ai.GetPoseSuggestionsAsync("goal"), Times.Once);
        mockSearchService.Verify(s => s.SearchForSingleNameAsync("Chair"), Times.Once);
        mockSearchService.Verify(s => s.SearchForSingleNameAsync("Tree"), Times.Once);
    }

    [Fact]
    public async Task ExecuteGetAiSuggestionsAsync_ReturnsCorrectPoses_WhenFound()
    {
        //ARRANGE
        var mockAiService = new Mock<IAIYogaService>();
        var mockSearchService = new Mock<IPoseSearchService>();

        var suggestedPoses = new List<string> { "Chair", "Tree" };
        var chairPose = new Domain.Entities.Pose("Chair", 2);
        var treePose = new Domain.Entities.Pose("Tree", 1);
        
        mockAiService.Setup(ai => ai.GetPoseSuggestionsAsync("goal")).ReturnsAsync(suggestedPoses);
        mockSearchService.Setup(s => s.SearchForSingleNameAsync("Chair"))
            .ReturnsAsync(chairPose);
        mockSearchService.Setup(s => s.SearchForSingleNameAsync("Tree"))
            .ReturnsAsync(treePose);
        
        var useCase = new SearchAiUseCase(mockAiService.Object, mockSearchService.Object);
        
        //ACT
        var result = await useCase.ExecuteGetAiSuggestionsAsync("goal");
        
        //ASSERT
        Assert.Equal(2, result.Count);
        Assert.Equal("Chair", result.First().PoseName);
        Assert.Equal("Tree", result.Last().PoseName);
    }

    [Fact]
    public async Task ExecuteGetAiSuggestionsAsync_HandlesNullPoses_Gracefully()
    {
        var mockAiService = new Mock<IAIYogaService>();
        var mockSearchService = new Mock<IPoseSearchService>();

        var suggestedPoses = new List<string> { "Chair", "Fake" };
        var chairPose = new Domain.Entities.Pose("Chair", 2);

        mockAiService.Setup(ai => ai.GetPoseSuggestionsAsync("goal"))
            .ReturnsAsync(suggestedPoses);
        mockSearchService.Setup(s => s.SearchForSingleNameAsync("Chair"))
            .ReturnsAsync(chairPose);
        mockSearchService.Setup(s => s.SearchForSingleNameAsync("Fake"))
            .ReturnsAsync((Domain.Entities.Pose)null);

        var useCase = new SearchAiUseCase(mockAiService.Object, mockSearchService.Object);
        
        //ACT
        var result = await useCase.ExecuteGetAiSuggestionsAsync("goal");
        
        //ASSERT
        Assert.Single(result);
        Assert.DoesNotContain(result, p => p.PoseName == "Fake");
        Assert.Contains(result, p => p.PoseName == "Chair");
    }
}