using Microsoft.AspNetCore.Mvc;
using Moq;
using YogaApp.Application.DTO;
using YogaApp.Web.Models;

namespace YogaApp.Web.Tests.Controllers.CategoryTests;

public class IndexActionTest : CategoryControllerTestBase
{
    [Fact]
    public async Task Index_ReturnsViewWithModel()
    {
        //ARRANGE 
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT
        var result = await Controller.Index();
        
        //ASSERT
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<List<AllCategoriesViewModel>>(viewResult.Model);
    }

    [Fact]
    public async Task Index_CallsGetAllCategoriesService()
    {
        //ARRANGE
        MockServices.Setup(s => s.GetAllCategoriesAsync())
            .ReturnsAsync(new List<GetAllCategoriesResponseDto>());
        
        //ACT 
        await Controller.Index();
        
        //ASSERT
        MockServices.Verify(s=> s.GetAllCategoriesAsync(), Times.Once());
    }
}