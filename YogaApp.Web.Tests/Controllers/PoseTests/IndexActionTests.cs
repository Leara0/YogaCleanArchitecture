using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllPoses;
using YogaApp.Web.Models;
using YogaApp.Web.Tests.Controllers.PoseController;

namespace YogaApp.Web.Tests.Controllers.PoseTests;

public class IndexActionTests: PoseControllerTestBase
{

    [Fact]
    public async Task Index_CallsGetAllPosesService()
    {
        //ARRANGE
        var mockDto = new PosesByDifficultyDto
        {
            EasyPoses = new List<GetAllPosesResponseDto>(),
            MediumPoses = new List<GetAllPosesResponseDto>(),
            HardPoses = new List<GetAllPosesResponseDto>()
        };
        MockServices.Setup(s => s.GetAllPosesAsync()).ReturnsAsync(mockDto);
        
        //ACT
        await Controller.Index();
        
        //ASSERT
        MockServices.Verify(s => s.GetAllPosesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task Index_ReturnsViewWithModel()
    {
        //ARRANGE
        //set up minimal service response
        var mockDto = new PosesByDifficultyDto
        {
            EasyPoses = new List<GetAllPosesResponseDto>(),
            MediumPoses = new List<GetAllPosesResponseDto>(),
            HardPoses = new List<GetAllPosesResponseDto>()
        };
        MockServices.Setup(s=> s.GetAllPosesAsync())
            .ReturnsAsync(mockDto);
        
        //ACT
        var result = await Controller.Index();
        
        //ASSERT
        //verify view
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<AllPosesByDifficultyViewModel>(viewResult.Model);
    }
}