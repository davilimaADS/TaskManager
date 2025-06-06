using FluentValidation;
using TaskManager.Communication.Response.ProjectResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Project.GetById
{
    public class GetProjectByIdUseCase : IGetProjectByIdUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;
        private readonly IValidator<Guid> _validator;

        public GetProjectByIdUseCase(
            IProjectRepository projectRepository,
            IUserContext userContext,
            IValidator<Guid> validator)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
            _validator = validator;
        }

        public async Task<GetProjectByIdResponse> ExecuteAsync(Guid id)
        {
            var result = await _validator.ValidateAsync(id);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var userId = _userContext.GetCurrentUserId();

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null || project.OwnerId != userId)
            {
                throw new ProjectNotFoundException(id);
            }

            return new GetProjectByIdResponse
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
