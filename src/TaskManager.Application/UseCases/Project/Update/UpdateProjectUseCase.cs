using FluentValidation;
using TaskManager.Communication.Request.ProjectRequest;
using TaskManager.Communication.Response.ProjectResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Exception.ExceptionBase;

namespace TaskManager.Application.UseCases.Project.Update
{
    public class UpdateProjectUseCase : IUpdateProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IValidator<UpdateProjectRequest> _validator;
        private readonly IUserContext _userContext;

        public UpdateProjectUseCase(IProjectRepository projectRepository,
                                    IValidator<UpdateProjectRequest> validator,
                                    IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _validator = validator;
            _userContext = userContext;
        }

        public async Task<UpdateProjectResponse> ExecuteAsync(Guid id, UpdateProjectRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errors);
            }

            var userId = _userContext.GetCurrentUserId();

            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null || project.OwnerId != userId)
            {
                throw new ProjectNotFoundException(id);
            }

            project.Name = request.Name;
            project.Description = request.Description;
            project.Status = request.Status;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.UpdateAsync(project);

            return new UpdateProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Status = project.Status,
                UpdatedAt = project.UpdatedAt ?? DateTime.UtcNow
            };
        }
    }
}
