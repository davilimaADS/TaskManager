using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;

namespace TaskManager.Application.UseCases.User.Create
{
    public interface ICreateUserUseCase
    {
        Task<CreateUserResponse> Execute(CreateUserRequest request);
    }
}
