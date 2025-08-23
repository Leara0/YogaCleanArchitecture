using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllDifficulties;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Web.Models;

namespace YogaApp.Web.Tests.Controllers.PoseController;

public class UpdatePoseActionTests : PoseControllerTestBase
{
    // ============================GET UPDATE TESTS==============================
    [Fact]
    public async Task UpdatePose_Get_CallsServiceWithCorrectId()
    {
        //ARRANGE
        var mockDto= new UpdatePoseResponseDto
        {
            PoseId = 5,
            PoseName = "Test",
            DifficultyOptions = new List<SelectOptionDto>(),
            CategoryOptions = new List<SelectOptionDto>()
        };
        MockServices.Setup(s => s.UpdatePoseAsync(5))
            .ReturnsAsync(mockDto);
        
        //ACT
        await Controller.UpdatePose(5);
        
        //ASSERT
        MockServices.Verify(s => s.UpdatePoseAsync(5), Times.Once());
    }

    [Fact]
    public async Task UpdateePose_Get_ReturnsViewWithModel()
    {
        //ARRANGE
        var mockDto = new UpdatePoseResponseDto
        {
            PoseId = 2,
            PoseName = "Tree",
            DifficultyId = 1,
            DifficultyOptions = new List<SelectOptionDto>(),
            CategoryOptions = new List<SelectOptionDto>()
        };
        MockServices.Setup(s => s.UpdatePoseAsync(2))
            .ReturnsAsync(mockDto);
        
        //ACT
        var result = await Controller.UpdatePose(2);
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<UpdatePoseViewModel>(viewResult.Model);
    }
    //======================POST UPDATE TESTS============================= 
    [Fact]
    public async Task UpdatePose_Post_ValidModel_RedirectsToDetails()
    {
        //ARRANGE
        var viewModel = new UpdatePoseViewModel
        {
            PoseId = 2,
            PoseName = "Tree",
            DifficultyId = 1,
        };
        MockServices.Setup(s => s.UpdatePoseToDbAsync(It.IsAny<UpdatePoseRequestToDbDto>()))
            .Returns(Task.CompletedTask);
        
        //ACT
        var result = await Controller.UpdatePose(viewModel);
        
        //ASSERT
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectResult.ActionName);
        Assert.Equal(2, redirectResult.RouteValues["id"]);
        MockServices.Verify(s => 
            s.UpdatePoseToDbAsync(It.IsAny<UpdatePoseRequestToDbDto>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdatePose_Post_InvalidModel_ReturnsViewWithErrors()
    {
        //ARRANGE
        var viewModel = new UpdatePoseViewModel { PoseName = "" };
        Controller.ModelState.AddModelError("PoseName", "Required");
        
        MockServices.Setup(s=> s.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<GetAllDifficultiesResponseDto>());
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        var result = await Controller.UpdatePose(viewModel);
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(viewModel, viewResult.Model);
        Assert.False(Controller.ModelState.IsValid);
        MockServices.Verify(s => 
            s.UpdatePoseToDbAsync(It.IsAny<UpdatePoseRequestToDbDto>()), Times.Never);
    }

    [Fact]
    public async Task UpdatePose_Post_DomainValidationFails_ReturnsViewWithErrors()
    {
        //ARRANGE
        var viewModel = new UpdatePoseViewModel
        {
            PoseId = 6,
            PoseName = "",
            DifficultyId = 1,
        };
        MockServices.Setup(s => s.UpdatePoseToDbAsync(It.IsAny<UpdatePoseRequestToDbDto>()))
            .ThrowsAsync(new ArgumentException("Name cannot be empty!"));
        MockServices.Setup(s => s.GetAllDifficultiesAsync())
            .ReturnsAsync(new List<GetAllDifficultiesResponseDto>());
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        var result = await Controller.UpdatePose(viewModel);
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(Controller.ModelState.IsValid);
        Assert.Contains("Name cannot be empty!", Controller.ModelState[""].Errors[0].ErrorMessage);
    }
}