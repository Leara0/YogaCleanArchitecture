namespace YogaApp.Application.UseCases.UpdatePose;

public interface IUpdatePoseUseCase
{
    Task<UpdatePoseRequestDto> ExecuteUpdatePoseAsync(int PoseId);
}