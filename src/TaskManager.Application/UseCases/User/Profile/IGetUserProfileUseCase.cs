using TaskManager.Communication.Response.UserResponse;

namespace TaskManager.Application.UseCases.User.Profile
{
    public interface IGetUserProfileUseCase
    {
        Task<UserProfileResponse> Execute(Guid userId);
    }
}
