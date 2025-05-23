using TaskManager.Application.Validators.UserValidators;
using TaskManager.Communication.Request.UserRequest;
using TaskManager.Communication.Response.UserResponse;
using TaskManager.Domain.Repositories.UserRepositories;
using TaskManager.Exception.ExceptionBase;



namespace TaskManager.Application.UseCases.User.Create
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
       
             private readonly IUserRepository _userRepository;
        private readonly CreateUserRequestValidator _validator;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validator = new CreateUserRequestValidator();
        }

        public async Task<CreateUserResponse> Execute(CreateUserRequest request)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var exists = await _userRepository.ExistsByEmail(request.Email);
            if (exists)
            {
                throw new ErrorOnValidateException(new List<string> { "Já existe um usuário com esse e-mail." });
            }

            var user = new Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.Create(user);

            return new CreateUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
    

