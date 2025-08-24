using Microsoft.AspNetCore.Mvc;
using YogaApp.Application.UseCases.GetDifficultyByDiffId;
using Moq;
using YogaApp.Web.Models;

namespace YogaApp.Web.Tests.Controllers.DifficultyTests;

public class DetailActionTest :DifficultyControllerTestBase
{
    [Fact]
    public async Task Details_ValidId_ReturnsViewWithModel()
    {
        //ARRANGE
        var mockDto = new GetDifficultyByIdResponseDto();
        MockServices.Setup(s => s.GetDifficultyByIdAsync(1))
            .ReturnsAsync(mockDto);
        
        //ACT
        var result = await Controller.Details(1);
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<DifficultyDetailViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task Details_CallsServiceWithCorrectId()
    {
        //ARRANGE
        var mockDto = new GetDifficultyByIdResponseDto();
        MockServices.Setup(s=> s.GetDifficultyByIdAsync(7))
            .ReturnsAsync(mockDto);
        
        //ACT
        await Controller.Details(7);
        
        //ASSERT
        MockServices.Verify(s => s.GetDifficultyByIdAsync(7), Times.Once); 
    }
}