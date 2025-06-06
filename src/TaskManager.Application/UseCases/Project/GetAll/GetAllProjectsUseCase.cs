using TaskManager.Communication.Response.ProjectResponse;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;

namespace TaskManager.Application.UseCases.Project.GetAll
{
    public class GetAllProjectsUseCase : IGetAllProjectsUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserContext _userContext;

        public GetAllProjectsUseCase( IProjectRepository projectRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _userContext = userContext;
        }
        public async Task<List<GetAllProjectsResponse>> Execute()
        {
            var userId = _userContext.GetCurrentUserId(); // <- vai lançar UnauthorizedAccessException se não estiver logado

            var projects = await _projectRepository.GetAllAsync(userId);

            return projects.Select(p => new GetAllProjectsResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                Status = p.Status.ToString()
            }).ToList();
        }
    }
}
