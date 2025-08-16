using Moq;
using YogaApp.Application.RespositoryInterfaces;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using YogaApp.Application.UseCases;
using YogaApp.Domain.Entities;
using YogaApp.Application.DTO;


namespace YogaApp.Application.Tests.UseCases;

public class CreatePoseUseCaseTests
{
   [Fact]
   public async Task ExecuteCreatePoseInDbAsync_ReturnsCorrectId()
   {
      //arrange
      //set up the mock repo with fake data
      var mockPoseRepo = new Mock<IPoseRepository>();
      mockPoseRepo.Setup(x => x.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()))
         .ReturnsAsync(30);
      
      //set up Use case and inject in mock repo
      var useCase = new CreatePoseUseCase(mockPoseRepo.Object);
      var dto = new CreatePoseRequestDto { PoseName = "Test", DifficultyId = 2 };
      
      //act - run the use case code
      var result = await useCase.ExecuteCreatePoseInDbAsync(dto);
      
      //assert
      Assert.Equal(30, result);
      //this checks that the use case actually called the mock repo and did it only one time
      mockPoseRepo.Verify(x => x.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()), Times.Once);
   }
}