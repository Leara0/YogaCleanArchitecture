namespace YogaApp.Application.UseCases.DeletePose;

public interface IDeletePoseByPoseIdUseCase
{
    Task ExecuteDeletePoseByPoseIdAsync(int poseId);
}