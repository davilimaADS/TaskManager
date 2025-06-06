using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;

namespace TaskManager.Application.UseCases.User.Login
{
    public interface ILoginUserUseCase
    {
        Task<LoginUserResponse> Execute(LoginUserRequest request);
    }
}
