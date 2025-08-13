namespace YogaApp.Application.UseCases.UpdatePose;

public interface IUpdatePoseUseCase
{
    Task<UpdatePoseRequest> ExecuteUpdatePoseAsync(int PoseId);
}