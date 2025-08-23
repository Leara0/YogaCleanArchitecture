using Microsoft.Extensions.Logging;
using Moq;
using YogaApp.Application.UseCaseInterfaces;
using YogaApp.Web.Controllers;

namespace YogaApp.Web.Tests.Controllers.PoseController;

public abstract class PoseControllerTestBase
{
    protected readonly Mock<IApplicationServices> MockServices;
    protected readonly Mock<ILogger<Web.Controllers.PoseController>> MockLogger;
    protected readonly Web.Controllers.PoseController Controller;

    protected PoseControllerTestBase()
    {
        MockServices = new Mock<IApplicationServices>();
        MockLogger = new Mock<ILogger<Web.Controllers.PoseController>>();
        Controller = new Web.Controllers.PoseController(MockLogger.Object, MockServices.Object);
    }
    

}