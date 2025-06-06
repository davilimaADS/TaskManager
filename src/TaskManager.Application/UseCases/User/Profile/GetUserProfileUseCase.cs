using TaskManager.Communication.Response.UserResponse;
using TaskManager.Domain.Repositories.UserRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.User.Profile
{
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileResponse> Execute(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
            {
                throw new UserNotFoundException(id);
            }

            return new UserProfileResponse
            {
                Id = user.Id, 
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
