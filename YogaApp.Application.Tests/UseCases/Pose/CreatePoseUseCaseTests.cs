using Moq;
using YogaApp.Application.RespositoryInterfaces;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using YogaApp.Application.UseCases;
using YogaApp.Domain.Entities;
using YogaApp.Application.DTO;


namespace YogaApp.Application.Tests.UseCases;

public class CreatePoseUseCaseTests
{
   [Fact]
   public async Task ExecuteCreatePoseInDbAsync_ReturnsCorrectId()
   {
      //ARRANGE
      //set up the mock repo
      var mockPoseRepo = new Mock<IPoseRepository>();
      var mockCatRepo = new Mock<ICategoryRepository>();
      //tell mock repos how to respond when given data
      mockPoseRepo.Setup(p => p.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()))
         .ReturnsAsync(30);
      
      //set up use case, repo injection, and sample dto
      var useCase = new CreatePoseUseCase(mockPoseRepo.Object, mockCatRepo.Object );
      var dto = new CreatePoseRequestDto { PoseName = "Test", DifficultyId = 2 };
      
      //ACT - run the use case code
      var result = await useCase.ExecuteCreatePoseInDbAsync(dto);
      
      //ASSERT
      Assert.Equal(30, result);
      //this checks that the use case actually called the mock repo and did it only one time
      mockPoseRepo.Verify(x => x.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()), Times.Once);
   }

   [Fact]
   public async Task ExecuteCreatePoseInDbAsync_CallsAddCategoryByPoseIdAsync_WhenCategoryExists()
   {
      //ARRANGE
      // set up mock repos
      var mockPoseRepo = new Mock<IPoseRepository>();
      var mockCatRepo = new Mock<ICategoryRepository>();
      
      //tell mock repos how to respond when given data
      mockPoseRepo.Setup(p => p.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()))
         .ReturnsAsync(5);
      mockCatRepo.Setup(c => c.AddCategoryByPoseIdAsync(It.IsAny<int>(), It.IsAny<List<int>>()))
         .Returns(Task.CompletedTask);
      
      //set up use case, repo injection, and sample dto
      var useCase = new CreatePoseUseCase(mockPoseRepo.Object, mockCatRepo.Object);
      var dto = new CreatePoseRequestDto 
         { 
            PoseName = "Test", 
            DifficultyId = 2, 
            CategoryIds = new List<int> {1, 2, 4}
         };
      
      //ACT - run the use case
      var result = await useCase.ExecuteCreatePoseInDbAsync(dto);
      
      //ASSERT
      //checks the category repo was called one time with the right parameters 
      mockCatRepo.Verify(x=>x.AddCategoryByPoseIdAsync(5, dto.CategoryIds), Times.Once);
   }

   [Fact]
   public async Task ExecuteCreatePoseInDbAsync_DoesNotCallAssCategory_WhenNoCategoriesExist()
   {
      //ARRANGE
      //set up mock repos
      var mockPoseRepo = new Mock<IPoseRepository>();
      var mockCatRepo = new Mock<ICategoryRepository>();
      
      //tell mock repo how to respond when given data
      mockPoseRepo.Setup(p => p.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>())).ReturnsAsync(16);
      
      //set up use case instance and give sample pose data
      var useCase = new CreatePoseUseCase(mockPoseRepo.Object, mockCatRepo.Object);
      var dto = new CreatePoseRequestDto
      {
         PoseName = "Mountain",
         DifficultyId = 1,
         CategoryIds = null
      };
      
      //ACT - run the use case
      await useCase.ExecuteCreatePoseInDbAsync(dto);
      
      //ASSERT
      //check pose repo was called one time and that cat repo was never called
      mockPoseRepo.Verify(p=> p.CreatePoseAsync(It.IsAny<Domain.Entities.Pose>()), Times.Once);
      mockCatRepo.Verify(c => c.AddCategoryByPoseIdAsync(It.IsAny<int>(), It.IsAny<List<int>>()), Times.Never);
   }
   
   [Fact]
}