namespace YogaApp.Application.UseCases.UpdatePose;

public interface IUpdatePoseUseCase
{
    Task<UpdatePoseResponseDto> ExecuteUpdatePoseAsync(int PoseId);
    
    Task ExecuteUpdatePoseToDbAsync(UpdatePoseRequestToDbDto dto);
}