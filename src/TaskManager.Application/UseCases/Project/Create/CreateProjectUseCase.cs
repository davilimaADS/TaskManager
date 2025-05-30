using TaskManager.Application.Validators.ProjectValidator;
using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Communication.Response.ProjectResponse;
using TaskManager.Domain.Enums;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Exception.ExceptionBase;


namespace TaskManager.Application.UseCases.Project.Create
{
    public class CreateProjectUseCase : ICreateProjectUseCase
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;
        private readonly CreateProjectRequestValidator _validator;

        public CreateProjectUseCase(IProjectRepository projectRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
            _validator = new CreateProjectRequestValidator();
        }

        public async Task<CreateProjectResponse> Execute(CreateProjectRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var ownerId = _userContext.GetCurrentUserId();

            var project = new Domain.Entities.Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                OwnerId = ownerId,
                CreatedAt = DateTime.UtcNow,
                Status = ProjectStatus.InProgress
            };

            await _projectRepository.AddAsync(project); // <- Use o nome correto do método

            return new CreateProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                Status = project.Status.ToString()
            };
        }
    }
}

