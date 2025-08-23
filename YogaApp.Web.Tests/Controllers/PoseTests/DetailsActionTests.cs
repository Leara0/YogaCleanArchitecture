using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.DTO;
using Moq;
using YogaApp.Web.Models;

namespace YogaApp.Web.Tests.Controllers.PoseController;

public class DetailsActionTests :PoseControllerTestBase
{
    [Fact]
    public async Task Details_ValidId_CallsServiceWithCorrectId()
    {
        //ARRANGE
        //minimal dto to satisfy controller parameters
        var mockDto = new GetPoseByIdResponseDto
        {
            PoseId = 5,
            PoseName = "Camel",
            DifficultyLink = new DifficultyLinkDto(),
            CategoryLink = new List<CategoryLinkDto>()
        };

        MockServices.Setup(s => s.GetPoseByIdAsync(5)).ReturnsAsync(mockDto); 
        
        //ACT
        var result = await Controller.Details(5);
        
        //ASSERT
        //verify service was called with correct ID
        MockServices.Verify(s=> s.GetPoseByIdAsync(5), Times.Once);
    }
    
    [Fact]
    public async Task Details_ValidId_ReturnsViewWithModel()
    {
        //ARRANGE
        //minimal dto to satisfy controller parameters
        var mockDto = new GetPoseByIdResponseDto
        {
            PoseId = 1,
            PoseName = "Mountain",
            DifficultyLink = new DifficultyLinkDto(),
            CategoryLink = new List<CategoryLinkDto>()
        };

        MockServices.Setup(s => s.GetPoseByIdAsync(1)).ReturnsAsync(mockDto); 
        
        //ACT
        var result = await Controller.Details(1);
        
        //ASSERT
        //verify view and result model
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<PoseDetailViewModel>(viewResult.Model);
    }
}