using Microsoft.Extensions.Logging;
using Moq;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Controllers;

namespace YogaApp.Web.Tests.Controllers.DifficultyTests;

public abstract class DifficultyControllerTestBase
{
    protected readonly Mock<IApplicationServices> MockServices;
    protected readonly Mock<ILogger<DifficultyController>> MockLogger;
    protected readonly DifficultyController Controller;

    protected DifficultyControllerTestBase()
    {
        MockServices = new Mock<IApplicationServices>();
        MockLogger = new Mock<ILogger<DifficultyController>>();
        Controller = new DifficultyController(MockLogger.Object, MockServices.Object);
    }

}