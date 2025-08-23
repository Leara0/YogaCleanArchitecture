using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllDifficulties;
using YogaApp.Web.Models;
using YogaApp.Web.Tests.Controllers.PoseController;

namespace YogaApp.Web.Tests.Controllers.PoseTests;

public class CreatePoseActionTests :PoseControllerTestBase
{
    [Fact]
    public async Task CreateNewPose_Get_ReturnsViewWithModel()
    {
        //ARRANGE 
        MockServices.Setup(s=> s.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<GetAllDifficultiesResponseDto>());
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        var result = await Controller.CreateNewPose();
        
        //ASSERT
        //checks controller returned view result (rather than redirect/JSON...) and casts to ViewResult type
        var viewResult = Assert.IsType<ViewResult>(result);
        //checks view received is the correct viewModel type
        Assert.IsType<CreatePoseViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task CreateNewPose_Pose_ValidModel_RedirectsToDetails()
    {
        //ARRANGE
        //set up valid input data
        var viewModel = new CreatePoseViewModel
        {
            PoseName = "Test Pose",
            DifficultyId = 1
        };
        
        //tells app layer how to respond when called (return 5 as pose Id)
        MockServices.Setup(s => s.CreatePoseInDbAsync(It.IsAny<CreatePoseRequestDto>()))
            .ReturnsAsync(5);
        
        //ACT
        //execute POST action with valid data
        var result = await Controller.CreateNewPose(viewModel);
        
        //ASSERT
        //verify successful creation behavior - controller redirects to Details page
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectResult.ActionName);
        Assert.Equal(5, redirectResult.RouteValues["id"]);
        
        //verify service was actually called
        MockServices.Verify(s => s.CreatePoseInDbAsync(It.IsAny<CreatePoseRequestDto>()), Times.Once);
    }

    [Fact]
    public async Task CreateNewPose_Post_InvalidModel_ReturnsViewWithErrors()
    {
        //ARRANGE
        //set up invalid input
        var viewModel = new CreatePoseViewModel { PoseName = "" };
        
        //simulate model validation error -normally done automatically by ASP.NET Core
        Controller.ModelState.AddModelError("PoseName", "Required");
        
        //mock services needed to repopulate form dropdowns
        MockServices.Setup(s=> s.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<GetAllDifficultiesResponseDto>());
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        //executes POSE with invalid data
        var result = await Controller.CreateNewPose(viewModel);
        
        //ASSERT
        //should return the form (ViewResult), not redirect
        var viewResult = Assert.IsType<ViewResult>(result);
        
        //should return the same model the user submitted (preserves their input)
        Assert.Same(viewModel, viewResult.Model);
        
        //ModelState should still be invalid
        Assert.False(Controller.ModelState.IsValid);
        
        //app layer shouldn't be called
        MockServices.Verify(s=> s.CreatePoseInDbAsync(It.IsAny<CreatePoseRequestDto>()), Times.Never);
    }

    [Fact]
    public async Task CreateNewPose_Post_ServiceThrowsException_ReturnsViewWithError()
    {
        //ARRANGE
        //set up service failure scenario
        var viewModel = new CreatePoseViewModel {PoseName = "Test Pose", DifficultyId = 1};
        
        //tell app layer how to respond to call (throw exception)
        MockServices.Setup(s => s.CreatePoseInDbAsync(It.IsAny<CreatePoseRequestDto>()))
            .ThrowsAsync(new ArgumentException("Domain validation failed"));
        
        MockServices.Setup(s => s.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<GetAllDifficultiesResponseDto>());
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        var result = await Controller.CreateNewPose(viewModel);
        
        //ASSERT
        //should return to form view (not crash or redirect)
        var viewResult = Assert.IsType<ViewResult>(result);
        
        //ModelState should be invalid due to the error
        Assert.False(Controller.ModelState.IsValid);
        
        //Error message should be added to ModelState for display to user
        Assert.Contains("Domain validation failed", Controller.ModelState[""].Errors[0].ErrorMessage);
    }
}