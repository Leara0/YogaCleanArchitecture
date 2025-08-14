namespace YogaApp.Application.UseCases.UpdatePose;

public interface IUpdatePoseUseCase
{
    Task<UpdatePoseResponseDto> ExecuteUpdatePoseAsync(int PoseId);
    
    Task<int> ExecuteUpdatePoseToDbAsync(UpdatePoseResponseDto dto);
}