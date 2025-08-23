using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Web.Tests.Controllers.PoseController;

namespace YogaApp.Web.Tests.Controllers.PoseTests;

public class DeletePoseActionTests : PoseControllerTestBase
{
    
    [Fact]
    public async Task DeletePose_CallsServiceWithCorrectId()
    {
        //ARRANGE
        MockServices.Setup(s => s.DeletePoseAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        
        //ACT
        await Controller.DeletePose(32);
        
        //ASSERT
        //verify correct Id was passed through
        MockServices.Verify(s=> s.DeletePoseAsync(32), Times.Once);
    }
    
    [Fact]
    public async Task DeletePose_ValidId_RedirectsToIndex()
    {
        //ARRANGE
        MockServices.Setup(s => s.DeletePoseAsync(7))
            .Returns(Task.CompletedTask);//simulate successful deletion
        
        //ACT
        var result = await Controller.DeletePose(7);
        
        //ASSERT
        //should redirect to Index page after successful deletion
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        
        //verify the service was called with correct ID
        MockServices.Verify(s => s.DeletePoseAsync(7), Times.Once);
    }
}