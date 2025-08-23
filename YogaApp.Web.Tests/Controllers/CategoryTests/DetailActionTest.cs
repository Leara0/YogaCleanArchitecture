using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Application.DTO;
using YogaApp.Domain.Entities;
using YogaApp.Web.Models;

namespace YogaApp.Web.Tests.Controllers.CategoryTests;

public class DetailActionTest :CategoryControllerTestBase
{
    [Fact]
    public async Task Details_ValidId_ReturnsViewWithModel()
    {
        //ARRANGE
        var mockDto = new GetCatByCatIdResponseDto();
        MockServices.Setup(s => s.GetCatByCatIdAsync(1))
            .ReturnsAsync(mockDto);
        
        //ACT
        var result = await Controller.Details(1);
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<CategoryDetailViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task Details_CallsServiceWithCorrectId()
    {
        //ARRANGE
        var mockDto = new GetCatByCatIdResponseDto();
        MockServices.Setup(s => s.GetCatByCatIdAsync(8))
            .ReturnsAsync(mockDto);
        
        //ACT
        await Controller.Details(8);
        
        //ASSERT
        MockServices.Verify(s => s.GetCatByCatIdAsync(8), Times.Once);
    }
}
