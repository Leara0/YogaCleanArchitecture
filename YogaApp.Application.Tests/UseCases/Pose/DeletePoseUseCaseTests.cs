using Moq;
using YogaApp.Application.RespositoryInterfaces;
using YogaApp.Application.UseCases.DeletePose;

namespace YogaApp.Application.Tests.UseCases.Pose;

public class DeletePoseUseCaseTests
{
    [Fact]
    public async Task ExecuteDeletePoseByPoseIdAsync_CallsCatRepoFirst_ThenPoseRepo()
    {
        //ARRANGE
        var mockPoseRepo = new Mock<IPoseRepository>();
        var mockCatRepo = new Mock<ICategoryRepository>();
        //create new list to hold the names of methods when they are called
        var callOrder = new List<string>();
        
        //tells mock repo how to respond when called (including to add something to the callOrder list when called)
        mockCatRepo.Setup(c => c.DeleteCategoriesByPoseIdAsync(It.IsAny<int>()))
            .Callback(() => callOrder.Add("Categories"))
            .Returns(Task.CompletedTask);
        mockPoseRepo.Setup(p => p.DeletePoseByPoseIdAsync(It.IsAny<int>()))
            .Callback(() => callOrder.Add("Pose"))
            .Returns(Task.CompletedTask);

        var useCase = new DeletePoseByPoseIdUseCase(mockPoseRepo.Object, mockCatRepo.Object);
        
        //ACT
        await useCase.ExecuteDeletePoseByPoseIdAsync(8);
        
        //ASSERT - the words in callOrder should be in the correct order if the repos were called in the correct order
        Assert.Equal(new[] {"Categories", "Pose"}, callOrder);
    }
}