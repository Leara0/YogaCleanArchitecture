using Microsoft.Extensions.Logging;
using Moq;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Controllers;

namespace YogaApp.Web.Tests.Controllers.CategoryTests;

public abstract class CategoryControllerTestBase
{
    protected readonly Mock<IApplicationServices> MockServices;
    protected readonly Mock<ILogger<CategoryController>> MockLogger;
    protected readonly CategoryController Controller;

    protected CategoryControllerTestBase()
    {
        MockServices = new Mock<IApplicationServices>();
        MockLogger = new Mock<ILogger<CategoryController>>();
        Controller = new CategoryController(MockLogger.Object, MockServices.Object);
    }
}